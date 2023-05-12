import { useEffect, useRef, useState } from "react";
import { ScrollView } from "react-native";
import DrawerLayout from "react-native-drawer-layout";
import appxios from "../../../components/AxiosInterceptor";
import { getMaxPage } from "../../../libs/functions/paging";
import { OrderStatus } from "../../../objects/enums/OrderStatus";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { BookViewModel } from "../../../objects/viewmodels/Books/BookViewModel";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import { paletteGrayShade5, paletteGreenShade1, paletteRed, primaryTint2 } from "../../../utils/color";
import endPont from "../../../utils/endPoints";
import { mockBooks, mockStaffCampaigns } from "../../../utils/mock";

export default function useStaffOrdersPage() {
    const scrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);
    const [refreshing, setRefreshing] = useState(false);

    const [currentPage, setCurrentPage] = useState(1);
    const [maxPage, setMaxPage] = useState(0);

    const [search, setSearch] = useState("");
    const [orders, setOrders] = useState<OrderViewModel[]>([]);
    const [orderStatus, setOrderStatus] = useState(0);

    const onPageNavigation = (page: number) => {
        getOrders(page);
        scrollViewRef.current?.scrollTo({
            y: 0
        });
    }

    const getOrders = (page: number) => {
        const query = new URLSearchParams();
        query.append("Page", page.toString());
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
            scrollViewRef,
        },
        paging: {
            currentPage,
            maxPage,
            onPageNavigation
        },
        ui: {
            loading,
            refreshing
        },
        event: {
            onSearchSubmit,
            onRefresh
        },
        data: {
            orders
        },
        input: {
            search:
            {
                value: search,
                set: setSearch
            },
            orderStatus: {
                value: orderStatus,
                set: setOrderStatus
            }
        }
    }
}