import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useRef, useState } from "react";
import { BackHandler, ScrollView } from "react-native";
import DrawerLayout from "react-native-drawer-layout";
import appxios from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import { getMaxPage } from "../../../libs/functions/paging";
import { StaffProductInCart } from "../../../objects/models/StaffProductInCart";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { MobileBookProductViewModel } from "../../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import endPont from "../../../utils/endPoints";

export function useCreateChooseProductsOrderBooksPage(props: StackScreenProps<ParamListBase>) {
    const params = props.route.params as { campaign: StaffCampaignMobilesViewModel, customer?: { name: string, phone: string, email: string } };
    const { staffCart, setStaffCart } = useAppContext();

    const filterBooksDrawerRef = useRef<DrawerLayout>(null);
    const booksScrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);

    const [currentPage, setCurrentPage] = useState(1);
    const [maxPage, setMaxPage] = useState(0);
    const [books, setBooks] = useState<MobileBookProductViewModel[]>([]);
    const [search, setSearch] = useState("");
    const onPageNavigation = (page: number) => {

    }
    const getBooks = (page: number) => {
        setLoading(true);
        const query = new URLSearchParams();
        query.append("CampaignId", params.campaign.id.toString());
        query.append("Page", page.toString());
        query.append("Size", "30");

        //console.log(query.toString());


        appxios.get<BaseResponsePagingModel<MobileBookProductViewModel>>(`${endPont.public.books.customer.products}?${query.toString()}`)
            .then(response => {
                setBooks(response.data.data);
                setCurrentPage(page);
                setMaxPage(getMaxPage(response.data.metadata.size, response.data.metadata.total));
                //console.log(response.data.data[0].imageUrl);
            })
            .finally(() => {
                setLoading(false);
            });
    }

    const onSearchSubmit = () => {
        getBooks(1);
        filterBooksDrawerRef.current?.closeDrawer();
    }

    const onBookSeleted = (book: MobileBookProductViewModel) => {
        const product = staffCart.find(p => p.id == book.id);
        if (product) {
            setStaffCart(staffCart.filter(p => p.id != book.id));
        }
        else {
            setStaffCart([...staffCart,
            {
                id: book.id,
                quantity: 1,
                saleQuantity : book.saleQuantity,
                imageUrl: book.imageUrl,
                salePrice: book.salePrice,
                coverPrice: book.book?.coverPrice,
                title: book.title,
                withPdf: false,
                withAudio: false,
                audioChecked: false,
                pdfChecked: false,
                audioExtraPrice: book.audioExtraPrice as number,
                pdfExtraPrice: book.pdfExtraPrice as number,
                issuerName: book.issuer?.user.name as string
            }]);
        }
    }

    useEffect(() => {
        //console.log(params);
        getBooks(1);
        // Clean up the listener when the component is unmounted
        return () => {
            setStaffCart([]);
        };
    }, []);

    return {
        ref: {
            filterBooksDrawerRef,
            booksScrollViewRef
        },
        input: {
            search: {
                value: search,
                set: setSearch
            },
        },
        event: {
            onBookSeleted,
            onSearchSubmit,
        },
        data: {
            books,
        },
        ui: {
            loading,
            setLoading
        },
        paging: {
            maxPage,
            currentPage,
            onPageNavigation
        }
    };
}
export function useCreateChooseProductsOrderSelectedBooksPage() {
    const { staffCart, setStaffCart } = useAppContext();
    // const onToggleChecked = (product: StaffProductInCart) => {
    //     const cloneCart = Object.create(staffCart) as StaffProductInCart[];
    //     const cloneProduct = cloneCart.find(p => p.id == product.id);
    //     if (cloneProduct) {
    //         cloneProduct.checked = !cloneProduct.checked;
    //     }
    //     setStaffCart(cloneCart);
    // }
    const removeFromCart = (productId: string) => {
        setStaffCart(staffCart.filter(p => p.id != productId));
    }
    const onQuantityChange = (productId: string, quantity: number) => {
        const cloneCart = Object.create(staffCart) as StaffProductInCart[];
        const productInCart = cloneCart.find(p => p.id == productId);
        if (productInCart) {
            productInCart.quantity = quantity;
        }
        setStaffCart(cloneCart);
    }
    return {
        event: {
            removeFromCart,
            onQuantityChange
        }
    };
}