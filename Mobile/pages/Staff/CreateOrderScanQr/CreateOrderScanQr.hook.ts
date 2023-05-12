import { ParamListBase, useRoute } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { BarCodeEvent, BarCodeScannedCallback } from "expo-barcode-scanner";
import { useState } from "react";
import { Toast } from "react-native-toast-message/lib/src/Toast";
import appxios from "../../../components/AxiosInterceptor";
import useRouter from "../../../libs/hook/useRouter";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import endPont from "../../../utils/endPoints";

export default function useCreateOrderScanQrPage(props: StackScreenProps<ParamListBase>) {
    const [scanQr, setScanQr] = useState(false);
    const [loading, setLoading] = useState(false);
    const { goBack, push } = useRouter();
    const onQrScaned = (e: BarCodeEvent) => {
        const json = JSON.parse(e.data) as { customerId: string, orderId: string };
        setScanQr(false);
        setLoading(true);
        appxios.get<OrderViewModel>(`${endPont.staff.orders.index}/${json.orderId}`).then(response => {
            push("OrderDetail", { order: response.data });

        }).finally(() => {
            setLoading(false);
        });
        // appxios.put(endPont.staff.orders.received,
        //     {
        //         id: json.orderId,
        //         note: ""
        //     }).then(response => {
        //         if (response.status == 200) {
        //             Toast.show({
        //                 text1: "Thông báo",
        //                 text2: "Thanh toán đơn hàng thành công"
        //             });
        //         }
        //         else {
        //             Toast.show({
        //                 text1: "Thông báo",
        //                 text2: "Thanh toán đơn hàng thất bại"
        //             });
        //         }
        //         goBack();
        //     }).finally(() => {
        //         setLoading(false);
        //     });
    }
    return {
        ui: {
            scanQr,
            setScanQr,
            loading
        },
        event: {
            onQrScaned
        }
    }
}