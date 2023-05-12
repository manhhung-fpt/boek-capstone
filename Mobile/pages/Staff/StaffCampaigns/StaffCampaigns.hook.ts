import { useEffect, useRef, useState } from "react";
import { ScrollView } from "react-native";
import appxios from "../../../components/AxiosInterceptor";
import useRouter from "../../../libs/hook/useRouter";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { HierarchicalStaffCampaignsViewModel } from "../../../objects/viewmodels/Campaigns/HierarchicalStaffCampaignsViewModel";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import { OrderViewModel } from "../../../objects/viewmodels/Orders/OrderViewModel";
import { SchedulesViewModel } from "../../../objects/viewmodels/Schedules/SchedulesViewModel";
import endPont from "../../../utils/endPoints";
import EndPont from "../../../utils/endPoints";
import { mockStaffCampaigns } from "../../../utils/mock";

export default function useStaffCampaignsPage() {
    const { navigate } = useRouter();
    const scrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);
    const [chooseSchedueModalVisible, setChooseSchedueModalVisible] = useState(false);

    const [campagins, setCampagins] = useState<HierarchicalStaffCampaignsViewModel[]>([]);
    const [schedueSelect, setSchedueSelect] = useState<SchedulesViewModel[]>([]);
    const [selectedCampaign, setSelectedCampaign] = useState<StaffCampaignMobilesViewModel>();

    const onCampaignPress = (campagin?: StaffCampaignMobilesViewModel) => {
        if (!campagin) {
            setChooseSchedueModalVisible(false);
            navigate("StaffCampagin", { campaign: selectedCampaign });
        }
        else if (campagin.isRecurring && campagin.schedules) {
            setSchedueSelect(campagin.schedules);
            setChooseSchedueModalVisible(true);
            setSelectedCampaign(campagin);
        }
        else {
            navigate("StaffCampagin", { campaign: campagin });
        }
    }
    const onChooseSchedueModalVisibleClose = () => {
        setChooseSchedueModalVisible(false);
        setSelectedCampaign({ ...selectedCampaign as StaffCampaignMobilesViewModel, selectedSchedule: undefined });
    }


    const getCampaigns = () => {
        setLoading(true);
        appxios.get<HierarchicalStaffCampaignsViewModel[]>(endPont.staff.campaigns).then(response => {
            setCampagins(response.data);
        }).finally(() => {
            setLoading(false);
        });
    }

    useEffect(() => {
        getCampaigns();
    }, []);
    
    return {
        ref: {
            scrollViewRef
        },
        event: {
            onCampaignPress,
            onChooseSchedueModalVisibleClose
        },
        ui: {
            loading,
            chooseSchedueModalVisible,
            setChooseSchedueModalVisible
        },
        input: {
            selectedCampaign:{
                value : selectedCampaign,
                set: setSelectedCampaign
            }
        },
        data: {
            campagins,
            schedueSelect
        }
    };
}