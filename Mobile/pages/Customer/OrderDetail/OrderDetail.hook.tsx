import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useState } from "react";
import { Alert, TouchableOpacity, View } from "react-native";
import Toast from "react-native-toast-message";
import Info from "../../../assets/SvgComponents/Info";
import appxios from "../../../components/AxiosInterceptor";
import useAuth from "../../../libs/hook/useAuth";
import useRouter from "../../../libs/hook/useRouter";
import { OrderStatus } from "../../../objects/enums/OrderStatus";
import { Role } from "../../../objects/enums/Role";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import { paletteGray, paletteGreenShade1, paletteRed, primaryTint2 } from "../../../utils/color";
import endPont from "../../../utils/endPoints";

export default function useOrderDetailPage(props: StackScreenProps<ParamListBase>) {
    const params = props.route.params as { order: OrderViewModel };
    const [infoModalVisible, setInfoModalVisible] = useState(false);
    const [order, setOrder] = useState(params.order);
    const [loading, setLoading] = useState(false);
    const { goBack } = useRouter();
    const { isInRole } = useAuth();
    const onOrderSubmit = () => {
        setLoading(true);
        appxios.put(endPont.staff.orders.received,
            {
                id: order.id,
                note: ""
            }).then(response => {
                if (response.status == 200) {
                    Toast.show({
                        text1: "Thông báo",
                        text2: "Thanh toán đơn hàng thành công"
                    });
                }
                else {
                    Toast.show({
                        text1: "Thông báo",
                        text2: "Thanh toán đơn hàng thất bại"
                    });
                }
                goBack();
            }).finally(() => {
                setLoading(false);
            });
    }
    const getStatusColor = () => {
        if (order.status == OrderStatus.Cancelled) {
            return paletteRed;
        }
        if (order.status == OrderStatus.Processing) {
            return paletteGray;
        }
        if (order.status == OrderStatus.PickUpAvailable) {
            return primaryTint2;
        }
        if (order.status == OrderStatus.Shipped || OrderStatus.Received) {
            return paletteGreenShade1;
        }
        return "blue";
    }

    useEffect(() => {
        if (isInRole([Role.customer.toString()])) {
            props.navigation.setOptions({
                headerRight: () =>
                    <View style={{ justifyContent: "center", alignItems: "center" }}>
                        <TouchableOpacity onPress={() => setInfoModalVisible(true)}>
                            <Info scale={60} fill="white" />
                        </TouchableOpacity>
                    </View>
            });
        }
    }, []);

    return {
        ui: {
            loading,
            getStatusColor,
            infoModalVisible,
            setInfoModalVisible
        },
        data: {
            order
        },
        event: {
            onOrderSubmit
        }
    };
}