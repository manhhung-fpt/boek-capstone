import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { Input } from "@rneui/base";
import { useEffect, useRef, useState } from "react";
import { Linking, TouchableOpacity, View } from "react-native";
import SelectDropdown from "react-native-select-dropdown";
import Toast from "react-native-toast-message";
import Info from "../../../assets/SvgComponents/Info";
import appxios from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import useRouter from "../../../libs/hook/useRouter";
import { CampaignFormat } from "../../../objects/enums/CampaignFormat";
import { District } from "../../../objects/enums/Districts";
import { OrderPayment } from "../../../objects/enums/OrderPayment";
import { OrderType } from "../../../objects/enums/OrderType";
import { Province } from "../../../objects/enums/Province";
import { Ward } from "../../../objects/enums/Ward";
import { CampaignInCart } from "../../../objects/models/CampaignInCart";
import { ProductInCart } from "../../../objects/models/ProductInCart";
import { CreateOrderDetailsRequestModel } from "../../../objects/requests/OrderDetails/CreateOrderDetailsRequestModel";
import { CreateCustomerPickUpOrderRequestModel } from "../../../objects/requests/Orders/CreateCustomerPickUpOrderRequestModel";
import { CreateShippingOrderRequestModel } from "../../../objects/requests/Orders/CreateShippingOrderRequestModel";
import { CreateZaloPayOrderRequestModel } from "../../../objects/requests/Orders/ZaloPay/CreateZaloPayOrderRequestModel";
import { ZaloPayOrderResponseModel } from "../../../objects/responses/Orders/OrderResponseModel";
import { OrderCalculationViewModel } from "../../../objects/viewmodels/Orders/Calculation/OrderCalculationViewModel";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import { CustomerViewModel } from "../../../objects/viewmodels/Users/customers/CustomerViewModel";
import endPont from "../../../utils/endPoints";

export default function useOrderConfirmPage(props: StackScreenProps<ParamListBase>) {
    const redirectUrl = "exp+boek://expo-development-client/?url=http%3A%2F%2F10.10.2.106%3A8081";

    const { cart, setCart, setTotalProductQuantity, totalProductQuantity, user } = useAppContext();
    const { navigate, popToTop } = useRouter();
    const params = props.route.params as { orderType: number, selectedCampaignId: number };
    const inputProvinceRef = useRef<SelectDropdown>(null);
    const inputDistrictRef = useRef<SelectDropdown>(null);
    const inputWardRef = useRef<SelectDropdown>(null);
    const inputAddressRef = useRef<Input>(null);

    const [infoModalVisible, setInfoModalVisible] = useState(false);
    const [loading, setLoading] = useState(false);
    const [opacity, setOpacity] = useState(1);

    const [provincesSelect, setProvincesSelect] = useState<Province[]>([]);
    const [districtSelect, setDistrictSelect] = useState<District[]>([]);
    const [wardSelect, setWardSelect] = useState<Ward[]>([]);

    const [province, setProvince] = useState<Province>();
    const [district, setDistrict] = useState<District>();
    const [ward, setWard] = useState<Ward>();
    const [address, setAddress] = useState("");

    const [paymentMethod, setPaymentMethod] = useState(OrderPayment.Cash);
    const [orderType, setOrderType] = useState(0);
    const [seltedCampaign, setSeltedCampaign] = useState<CampaignInCart>();
    const [provisional, setProvisional] = useState(0);
    const [calculation, setCalculation] = useState<OrderCalculationViewModel>();
    const [customer, setCustomer] = useState<CustomerViewModel>();


    const onProvinceSelected = (selectedProvince: Province) => {
        if (!province || province && province.code != selectedProvince.code) {
            setProvince(selectedProvince);
            setDistrict(undefined);
            setWard(undefined);
            inputDistrictRef.current?.reset();
            setLoading(true);
            appxios.get<District[]>(`${endPont.public.provinces}/${selectedProvince.code}${endPont.lead.districts}`).then(response => {
                setDistrictSelect(response.data);
                setLoading(false);
            });
        }
    }
    const onDistrictSelected = (seletedDistrict: District) => {
        if (!district || district.code != seletedDistrict.code) {
            setDistrict(seletedDistrict);
            setWard(undefined);
            inputWardRef.current?.reset();
            setLoading(true);
            appxios.get<Ward[]>(`${endPont.public.districts}/${seletedDistrict.code}${endPont.lead.ward}`).then(response => {
                setWardSelect(response.data);
                setLoading(false);
            });
        }
    }
    const onDistrictSelectedFocus = () => {
        if (!province) {
            Toast.show({
                text1: "Thông báo",
                text2: "Vui lòng chọn tỉnh trước"
            });
            inputDistrictRef.current?.closeDropdown();

        }
    }
    const onWardSelected = (selectedWard: Ward) => {
        if (!ward || ward.code != selectedWard.code) {
            setWard(selectedWard);
        }
    }
    const onWardSelectedFocus = () => {
        if (!district) {
            Toast.show({
                text1: "Thông báo",
                text2: "Vui lòng chọn quận trước"
            });
            inputWardRef.current?.closeDropdown();
        }
    }

    const getProductFinalPrice = (product: ProductInCart) => {
        let productPrice = product.salePrice;
        if (product.audioChecked) {
            productPrice += product.audioExtraPrice;
        }
        if (product.pdfChecked) {
            productPrice += product.pdfExtraPrice;
        }
        productPrice *= product.quantity;
        return productPrice;
    }
    const removeCampaignFromCart = () => {
        let subTotal = 0;
        seltedCampaign?.issuersInCart.map(i => i.productsInCart.map(product => {
            subTotal += product.quantity;
        }));
        setTotalProductQuantity(totalProductQuantity - subTotal);
        setCart(cart.filter(c => c.campaign.id != seltedCampaign?.campaign.id));
    }
    const orderSuccess = (name: string) => {
        popToTop();
        navigate("Orders", { name: name });
        removeCampaignFromCart();
    }

    const onSumbit = async () => {
        setLoading(true);
        const orderDetails: CreateOrderDetailsRequestModel[] = [];
        seltedCampaign?.issuersInCart.map(issuer => issuer.productsInCart.map(product => {
            orderDetails.push({
                bookProductId: product.id,
                quantity: product.quantity,
                withAudio: product.withAudio,
                withPdf: product.withPdf
            });
        }));

        if (params.orderType == OrderType.PickUp) {
            if (paymentMethod == OrderPayment.Cash) {
                const request: CreateCustomerPickUpOrderRequestModel = {
                    campaignId: seltedCampaign?.campaign.id,
                    payment: paymentMethod,
                    orderDetails: orderDetails
                };
                appxios.post<OrderViewModel[]>(endPont.orders.customer.pickUp, request).then((response) => {
                    console.log(response.data);
                    if (response.status == 200) {
                        orderSuccess("CounterOrders");
                    }

                }).finally(() => {
                    setLoading(false);
                });
            }
            else if (paymentMethod == OrderPayment.ZaloPay) {
                const request: CreateZaloPayOrderRequestModel = {
                    type: orderType,
                    campaignId: seltedCampaign?.campaign.id,
                    customerId: customer?.user.id,
                    customerEmail: customer?.user.email,
                    customerName: customer?.user.name,
                    customerPhone: customer?.user.phone,
                    freight: calculation?.freight,
                    orderDetails: orderDetails,
                    payment: OrderPayment.ZaloPay,
                    redirectUrl: redirectUrl
                };
                appxios.post<ZaloPayOrderResponseModel>(endPont.orders.zaloPay, request).then(async response => {
                    await Linking.openURL(response.data.order_url).catch(err => console.error("Couldn't load page", err));
                    
                }).finally(() => {
                    setLoading(false);
                    orderSuccess("CounterOrders");
                });
            }
        }
        if (params.orderType == OrderType.Shipping) {
            if (customer) {
                const request: CreateShippingOrderRequestModel = {
                    campaignId: seltedCampaign?.campaign.id,
                    payment: paymentMethod,
                    orderDetails: orderDetails,
                    freight: calculation?.freight,
                    addressRequest: {
                        detail: address,
                        districtCode: district?.code as number,
                        provinceCode: province?.code as number,
                        wardCode: ward?.code as number
                    }
                };
                if (paymentMethod == OrderPayment.Cash) {
                    appxios.post<OrderViewModel[]>(endPont.orders.customer.shipping, request).then((response) => {
                        console.log(response.data);
                        if (response.status == 200) {
                            orderSuccess("DeliveryOrders");
                        }

                    }).finally(() => {
                        setLoading(false);
                    });
                }
                else if (paymentMethod == OrderPayment.ZaloPay) {
                    const request: CreateZaloPayOrderRequestModel = {
                        type: orderType,
                        campaignId: seltedCampaign?.campaign.id,
                        customerId: customer?.user.id,
                        customerEmail: customer?.user.email,
                        customerName: customer?.user.name,
                        customerPhone: customer?.user.phone,
                        freight: calculation?.freight,
                        orderDetails: orderDetails,
                        payment: OrderPayment.ZaloPay,
                        redirectUrl: redirectUrl,
                        addressRequest: {
                            detail: address,
                            districtCode: district?.code as number,
                            provinceCode: province?.code as number,
                            wardCode: ward?.code as number
                        }
                    };
                    appxios.post<ZaloPayOrderResponseModel>(endPont.orders.zaloPay, request).then(async response => {
                        console.log(response.data.order_url);
                        await Linking.openURL(response.data.order_url).catch(err => console.error("Couldn't load page", err));
                    }).finally(() => {
                        setLoading(false);
                        orderSuccess("DeliveryOrders");
                    });
                }
            }
        }
    }

    useEffect(() => {
        setOrderType(params.orderType);
        const campaign = cart.find(c => c.campaign.id == params.selectedCampaignId)
        setSeltedCampaign(campaign);
        let provisionalPrice = 0;
        campaign?.issuersInCart.map(i => i.productsInCart.map(product => {
            let productPrice = product.salePrice;
            if (product.audioChecked) {
                productPrice += product.audioExtraPrice;
            }
            if (product.pdfChecked) {
                productPrice += product.pdfExtraPrice;
            }
            productPrice *= product.quantity;
            provisionalPrice += productPrice;
        }));
        setProvisional(provisionalPrice);
        props.navigation.setOptions({
            headerRight: () =>
                <View style={{ justifyContent: "center", alignItems: "center" }}>
                    <TouchableOpacity onPress={() => setInfoModalVisible(true)}>
                        <Info scale={60} fill="white" />
                    </TouchableOpacity>
                </View>
        });

        setLoading(true);
        const orderDetails: CreateOrderDetailsRequestModel[] = [];
        campaign?.issuersInCart.map(issuer => issuer.productsInCart.map(product => {
            orderDetails.push({
                bookProductId: product.id,
                quantity: product.quantity,
                withAudio: product.audioChecked,
                withPdf: product.pdfChecked
            });
        }));
        appxios.get<CustomerViewModel>(endPont.users.me).then(async userResponse => {
            setCustomer(userResponse.data);
            if (params.orderType == OrderType.Shipping) {
                const request = {
                    campaignId: campaign?.campaign.id,
                    addressRequest: {
                        detail: userResponse.data.user.addressViewModel.detail,
                        provinceCode: userResponse.data.user.addressViewModel.provinceCode,
                        districtCode: userResponse.data.user.addressViewModel.districtCode,
                        wardCode: userResponse.data.user.addressViewModel.wardCode
                    },
                    orderDetails: orderDetails
                };
                const calcResponse = await appxios.post<OrderCalculationViewModel>(endPont.orders.calculation.shipping, request);
                setCalculation(calcResponse.data);
                const getUserresponse = await appxios.get<CustomerViewModel>(endPont.users.me);
                const getProvinceResponse = await appxios.get<Province[]>(endPont.public.provinces);
                const districtsResponse = await appxios.get<District[]>(`${endPont.public.provinces}/${getUserresponse.data.user.addressViewModel.provinceCode}${endPont.lead.districts}`);
                const wardsResponse = await appxios.get<Ward[]>(`${endPont.public.districts}/${getUserresponse.data.user.addressViewModel.districtCode}${endPont.lead.ward} `);
                setProvincesSelect(getProvinceResponse.data);
                setDistrictSelect(districtsResponse.data);
                setWardSelect(wardsResponse.data);
                setProvince(getProvinceResponse.data.find(p => p.code == getUserresponse.data.user.addressViewModel.provinceCode));
                setDistrict(districtsResponse.data.find(d => d.code == getUserresponse.data.user.addressViewModel.districtCode));
                setWard(wardsResponse.data.find(w => w.code == getUserresponse.data.user.addressViewModel.wardCode));
                setAddress(getUserresponse.data.user.addressViewModel.detail);
                setLoading(false);
                setOpacity(0.6);
            }
            else {
                const request = {
                    campaignId: campaign?.campaign.id,
                    orderDetails: orderDetails
                };
                appxios.post<OrderCalculationViewModel>(endPont.orders.calculation.pickUp, request).then(response => {
                    setCalculation(response.data);
                }).finally(() => {
                    setLoading(false);
                });
            }
        });
    }, []);

    return {
        ref: {
            inputDistrictRef,
            inputWardRef,
            inputProvinceRef,
            inputAddressRef
        },
        getProductFinalPrice,
        event: {
            onSumbit,
            onProvinceSelected,
            onDistrictSelected,
            onDistrictSelectedFocus,
            onWardSelected,
            onWardSelectedFocus
        },
        data: {
            orderType,
            seltedCampaign,
            provisional,
            calculation,
            provincesSelect,
            districtSelect,
            wardSelect,
        },
        input: {
            paymentMethod: {
                value: paymentMethod,
                set: setPaymentMethod
            },
            province: {
                value: province,
                set: setProvince
            },
            district: {
                value: district,
                set: setDistrict
            },
            ward: {
                value: ward,
                set: setWard
            },
            address: {
                value: address,
                set: setAddress
            },
        },
        ui: {
            infoModalVisible,
            setInfoModalVisible,
            loading,
            opacity
        },
    }
}