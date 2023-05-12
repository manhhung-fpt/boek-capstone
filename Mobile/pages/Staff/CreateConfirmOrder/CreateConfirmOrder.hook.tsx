import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useState } from "react";
import { TouchableOpacity, View } from "react-native";
import Info from "../../../assets/SvgComponents/Info";
import appxios from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import useRouter from "../../../libs/hook/useRouter";
import { OrderPayment } from "../../../objects/enums/OrderPayment";
import { CreateOrderDetailsRequestModel } from "../../../objects/requests/OrderDetails/CreateOrderDetailsRequestModel";
import { CreateCustomerPickUpOrderRequestModel } from "../../../objects/requests/Orders/CreateCustomerPickUpOrderRequestModel";
import { CreateStaffPickUpOrderRequestModel } from "../../../objects/requests/Orders/CreateStaffPickUpOrderRequestModel";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import { OrderCalculationViewModel } from "../../../objects/viewmodels/Orders/Calculation/OrderCalculationViewModel";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import endPont from "../../../utils/endPoints";

export default function useCreateConfirmOrderPage(props: StackScreenProps<ParamListBase>) {
    const [infoModalVisible, setInfoModalVisible] = useState(false);
    const [loading, setLoading] = useState(false);
    const params = props.route.params as { campaign: StaffCampaignMobilesViewModel, customer?: { id?: string; name: string, phone: string, email: string } };
    const [paymentMethod, setPaymentMethod] = useState(OrderPayment.Cash);
    const [calculation, setCalculation] = useState<OrderCalculationViewModel>();
    const { staffCart } = useAppContext();
    const { pop } = useRouter();

    const onSubmit = () => {
        const orderDetails: CreateOrderDetailsRequestModel[] = [];
        staffCart.map(item =>
            orderDetails.push({
                bookProductId: item.id,
                quantity: item.quantity,
                withAudio: item.audioChecked,
                withPdf: item.pdfChecked,
            }));
        const request: CreateStaffPickUpOrderRequestModel = {
            campaignId: params.campaign.id,
            address: params.campaign.isRecurring ? params.campaign.selectedSchedule?.address : params.campaign.address,
            payment: paymentMethod,
            orderDetails: orderDetails,
            customerEmail: params.customer?.email,
            customerId: params.customer?.id,
            customerName: params.customer?.name,
            customerPhone: params.customer?.phone
        };
        //console.log(request);
        setLoading(true);
        appxios.post(endPont.staff.orders.index, request).then(response => {
            //console.log(response.data);

            if (response.status == 200) {
                if (params.customer) {
                    if (params.customer.id) {
                        pop(5);
                    }
                    else {
                        pop(3);
                    }
                }
                else {
                    pop(4);
                }

            }
            setLoading(false);
        });
    }
    useEffect(() => {
        //console.log(params.schedue);

        // props.navigation.setOptions({
        //     headerRight: () =>
        //         <View style={{ justifyContent: "center", alignItems: "center" }}>
        //             <TouchableOpacity onPress={() => setInfoModalVisible(true)}>
        //                 <Info scale={60} fill="white" />
        //             </TouchableOpacity>
        //         </View>
        // });
        const orderDetails: CreateOrderDetailsRequestModel[] = [];
        staffCart.map(item => {
            orderDetails.push({
                bookProductId: item.id,
                quantity: item.quantity,
                withAudio: item.audioChecked,
                withPdf: item.pdfChecked
            });
        })
        const request = {
            campaignId: params?.campaign.id,
            orderDetails: orderDetails
        };
        setLoading(true);
        appxios.post<OrderCalculationViewModel>(endPont.orders.calculation.pickUp, request).then(response => {
            setCalculation(response.data);
        }).finally(() => {
            setLoading(false);
        });
    }, []);
    return {
        event: {
            onSubmit
        },
        input: {
            paymentMethod: {
                value: paymentMethod,
                set: setPaymentMethod
            }
        },
        data: {
            params,
            calculation
        },
        ui: {
            loading,
            infoModalVisible,
            setInfoModalVisible
        }
    };
}