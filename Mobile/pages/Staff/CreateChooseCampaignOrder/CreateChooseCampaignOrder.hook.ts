import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useRef, useState } from "react";
import { ScrollView } from "react-native";
import appxios from "../../../components/AxiosInterceptor";
import useRouter from "../../../libs/hook/useRouter";
import { HierarchicalStaffCampaignsViewModel } from "../../../objects/viewmodels/Campaigns/HierarchicalStaffCampaignsViewModel";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import { SchedulesViewModel } from "../../../objects/viewmodels/Schedules/SchedulesViewModel";
import endPont from "../../../utils/endPoints";

export default function useCreateChooseCampaignOrderPage(props: StackScreenProps<ParamListBase>) {
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
        ui: {
            loading,
            chooseSchedueModalVisible
        },
        data: {
            campagins,
            schedueSelect
        },
        event: {
            onChooseSchedueModalVisibleClose,
            onCampaignPress
        },
        input: {
            selectedCampaign: {
                value: selectedCampaign,
                set: setSelectedCampaign
            }
        },
    };
}