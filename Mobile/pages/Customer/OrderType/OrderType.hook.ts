import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useState } from "react";
import useAppContext from "../../../context/Context";
import { CampaignInCart } from "../../../objects/models/CampaignInCart";
import { CampaignMobileViewModel } from "../../../objects/viewmodels/Campaigns/Mobile/CampaignMobileViewModel";

export default function useOrderTypePage(props: StackScreenProps<ParamListBase>) {
    const { cart } = useAppContext();
    const [orderType, setOrderType] = useState(0);
    const [campaign, setCampaign] = useState<CampaignInCart>();
    const params = props.route.params as { seletedCampaignId: number };
    useEffect(() => {
        const c = cart.find(c => c.campaign.id == params.seletedCampaignId);
        console.log(c);
        setCampaign(c);
    }, []);
    return {
        data: {
            params,
            campaign
        },
        input: {
            orderType: {
                value: orderType,
                set: setOrderType
            }
        },
        ui: {
        }
    }
}