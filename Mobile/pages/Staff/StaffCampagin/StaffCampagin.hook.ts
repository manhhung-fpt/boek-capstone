import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useContext, useEffect, useRef, useState } from "react";
import { ScrollView } from "react-native";
import DrawerLayout from "react-native-drawer-layout";
import appxios from "../../../components/AxiosInterceptor";
import { getMaxPage } from "../../../libs/functions/paging";
import { OrderStatus } from "../../../objects/enums/OrderStatus";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { MobileBookProductViewModel } from "../../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel";
import { BookViewModel } from "../../../objects/viewmodels/Books/BookViewModel";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import { paletteGrayShade5, paletteGreenShade1, paletteRed, primaryTint2 } from "../../../utils/color";
import endPont from "../../../utils/endPoints";
import { mockBooks } from "../../../utils/mock";

export function useStaffBooksPage() {
    const booksScrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);

    const [search, setSearch] = useState("");

    const [books, setBooks] = useState<MobileBookProductViewModel[]>([]);
    const [currentPage, setCurrentPage] = useState(1);
    const [maxPage, setMaxPage] = useState(100);
    const onPageNavigation = (page: number) => {
        setCurrentPage(page)
        booksScrollViewRef.current?.scrollTo({
            y: 0,
            animated: true
        });
    }

    useEffect(() => {
        setBooks(mockBooks);
    }, []);
    return {
        loading,
        ref: {
            booksScrollViewRef
        },
        paging: {
            currentPage,
            maxPage,
            onPageNavigation
        },
        input: {
            search: {
                value: search,
                set: setSearch
            }
        },
        data: {
            books
        }
    }
}
export function useStaffCampaignOrderPage(props: StackScreenProps<ParamListBase>) {
    const params = props.route.params as { campaign: StaffCampaignMobilesViewModel };
    const campaginsScrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);
    const [refreshing, setRefreshing] = useState(false);

    const [orderStatus, setOrderStatus] = useState(0);


    const [orders, setOrders] = useState<OrderViewModel[]>([]);
    const [search, setSearch] = useState("");

    const [currentPage, setCurrentPage] = useState(1);
    const [maxPage, setMaxPage] = useState(0);

    const getStatusBackgrundColor = (statusId: number) => {
        if (statusId == OrderStatus.Cancelled) {
            return paletteRed;
        }
        if (statusId == OrderStatus.Processing) {
            return paletteGrayShade5;
        }
        if (statusId == OrderStatus.Shipping) {
            return primaryTint2;
        }
        if (statusId == OrderStatus.Shipped) {
            return paletteGreenShade1;
        }
        return "blue";
    }

    const onPageNavigation = (page: number) => {
        getOrders(page);
        campaginsScrollViewRef.current?.scrollTo({
            y: 0,
            animated: true
        });
    }
    const onOrderDetailPress = () => {
    }
    const onSearchSubmit = () => {
        getOrders(1);
    }
    const getOrders = (page: number) => {
        const query = new URLSearchParams();
        query.append("Page", page.toString());
        query.append("CampaignId", params.campaign.id.toString());
        query.append("Size", "30");
        if (orderStatus != 0) {
            query.append("Status", orderStatus.toString());
        }
        if (search != "") {
            query.append("Code", search);
        }
        setLoading(true);
        appxios.get<BaseResponsePagingModel<OrderViewModel>>(`${endPont.staff.orders.index}?${query.toString()}`).then(response => {
            setOrders(response.data.data);
            setCurrentPage(page);
            setMaxPage(getMaxPage(response.data.metadata.size, response.data.metadata.total));
        }).finally(() =>
            setLoading(false));
    }

    const onRefresh = () => {
        setRefreshing(true);
        getOrders(currentPage);
        setRefreshing(false);
    }

    useEffect(() => {
        getOrders(1);
    }, [orderStatus]);
    return {
        ref: {
            campaginsScrollViewRef,
        },
        event: {
            onOrderDetailPress,
            onSearchSubmit,
            onRefresh
        },
        data: {
            orders
        },
        ui: {
            loading,
            refreshing,
            getStatusBackgrundColor
        },
        paging: {
            currentPage,
            maxPage,
            onPageNavigation
        },
        input: {
            orderStatus: {
                value: orderStatus,
                set: setOrderStatus
            },
            search:
            {
                value: search,
                set: setSearch
            }
        }
    };
}