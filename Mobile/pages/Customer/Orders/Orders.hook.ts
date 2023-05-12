import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useRef, useState } from "react";
import { Alert, ScrollView } from "react-native";
import appxios from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import { getMaxPage } from "../../../libs/functions/paging";
import { OrderStatus } from "../../../objects/enums/OrderStatus";
import { OrderType } from "../../../objects/enums/OrderType";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import { paletteGray, paletteGrayShade2, paletteGrayShade5, paletteGrayShade6, paletteGreenShade1, paletteOrange, palettePink, paletteRed, primaryTint2, primaryTint4, primaryTint6 } from "../../../utils/color";
import endPont from "../../../utils/endPoints";

export function useDeliveryOrdersPage() {
    const ordersScrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);
    const [refreshing, setRefreshing] = useState(false);

    const [confirmOrderSubmitModalVisible, setConfirmOrderSubmitModalVisible] = useState(false);

    const [currentPage, setCurrentPage] = useState(1);
    const [maxPage, setMaxPage] = useState(0);
    const [orderStatus, setOrderStatus] = useState(0);
    const [search, setSearch] = useState("");

    const [orders, setOrders] = useState<OrderViewModel[]>([]);

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
        ordersScrollViewRef.current?.scrollTo({
            y: 0,
            animated: true
        });
    }
    const getOrders = (page: number) => {
        setLoading(true);
        const query = new URLSearchParams();
        query.append("Page", page.toString());
        query.append("Size", "30");
        query.append("Type", OrderType.Shipping.toString());
        if (orderStatus != 0) {
            query.append("Status", orderStatus.toString());
        }
        if (search != "") {
            query.append("Code", search);
        }
        appxios.get<BaseResponsePagingModel<OrderViewModel>>(`${endPont.orders.index}?${query.toString()}`).then(response => {
            //console.log(response.data);

            setOrders(response.data.data);
            setCurrentPage(page);
            setMaxPage(getMaxPage(response.data.metadata.size, response.data.metadata.total));
        }).finally(() => {
            setLoading(false);
        });
    }

    const onSearchSubmit = () => {
        getOrders(1);
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
            ordersScrollViewRef
        },
        ui: {
            getStatusBackgrundColor,
            loading,
            refreshing,
            confirmOrderSubmitModalVisible,
            setConfirmOrderSubmitModalVisible,
        },
        data: {
            orders
        },
        input: {
            orderStatus: {
                value: orderStatus,
                set: setOrderStatus
            },
            search: {
                value: search,
                set: setSearch
            }
        },
        event: {
            onSearchSubmit,
            onRefresh
        },
        paging: {
            currentPage,
            maxPage,
            onPageNavigation
        }
    }
}
export function useCounterOrdersPage() {
    const ordersScrollViewRef = useRef<ScrollView>(null);

    const { user } = useAppContext();

    const [loading, setLoading] = useState(false);
    const [refreshing, setRefreshing] = useState(false);

    const [confirmOrderSubmitModalVisible, setConfirmOrderSubmitModalVisible] = useState(false);
    const [qrModalVisible, setQrModalVisible] = useState(false);

    const [currentPage, setCurrentPage] = useState(1);
    const [maxPage, setMaxPage] = useState(0);
    const [orderStatus, setOrderStatus] = useState(0);
    const [qrString, setQrString] = useState("");
    const [search, setSearch] = useState("");

    const [orders, setOrders] = useState<OrderViewModel[]>([]);

    const getStatusBackgrundColor = (statusId: number) => {
        if (statusId == OrderStatus.Cancelled) {
            return paletteRed;
        }
        if (statusId == OrderStatus.Processing) {
            return paletteGray;
        }
        if (statusId == OrderStatus.PickUpAvailable) {
            return primaryTint2;
        }
        if (statusId == OrderStatus.Shipped || OrderStatus.Received) {
            return paletteGreenShade1;
        }

        return "blue";
    }

    const onPageNavigation = (page: number) => {
        getOrders(page);
        ordersScrollViewRef.current?.scrollTo({
            y: 0,
            animated: true
        });
    }
    const getOrders = (page: number) => {
        setLoading(true);
        const query = new URLSearchParams();
        query.append("Page", page.toString());
        query.append("Size", "30");
        query.append("Type", OrderType.PickUp.toString());
        if (orderStatus != 0) {
            query.append("Status", orderStatus.toString());
        }
        if (search != "") {
            query.append("Code", search);
        }
        appxios.get<BaseResponsePagingModel<OrderViewModel>>(`${endPont.orders.index}?${query.toString()}`).then(response => {
            //console.log(response.data);

            setOrders(response.data.data);
            setCurrentPage(page);
            setMaxPage(getMaxPage(response.data.metadata.size, response.data.metadata.total));
        }).finally(() => {
            setLoading(false);
        });
    }
    const onSearchSubmit = () => {
        getOrders(1);
    }

    const onShowQrPress = (orderId: string) => {
        setQrString(JSON.stringify({
            customerId: user?.id,
            orderId: orderId
        }));
        setQrModalVisible(true);
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
            ordersScrollViewRef
        },
        ui: {
            getStatusBackgrundColor,
            loading,
            confirmOrderSubmitModalVisible,
            setConfirmOrderSubmitModalVisible,
            qrModalVisible,
            setQrModalVisible,
            refreshing
        },
        data: {
            orders,
            qrString
        },
        input: {
            orderStatus: {
                value: orderStatus,
                set: setOrderStatus
            },
            search: {
                value: search,
                set: setSearch
            }
        },
        event: {
            onShowQrPress,
            onSearchSubmit,
            onRefresh
        },
        paging: {
            currentPage,
            maxPage,
            onPageNavigation
        }
    }
}