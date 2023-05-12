import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useState } from "react";
import { CampaignMobileViewModel } from "../../../objects/viewmodels/Campaigns/Mobile/CampaignMobileViewModel";

export default function useRecurringCampaignPage(props: StackScreenProps<ParamListBase>) {
    const [campaign, setCampaign] = useState<CampaignMobileViewModel>();
    useEffect(() => {
        const params = props.route.params as { data: CampaignMobileViewModel };
        setCampaign(params.data);
    }, []);
    return {
        data: {
            campaign
        }
    }
}