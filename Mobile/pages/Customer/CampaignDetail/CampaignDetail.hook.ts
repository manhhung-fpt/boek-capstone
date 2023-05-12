import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { createElement, useEffect, useRef, useState } from "react";
import { NativeScrollEvent, NativeSyntheticEvent, ScrollView } from "react-native";
import appxios from "../../../components/AxiosInterceptor";
import CardHeader from "../../../components/CartHeader/CardHeader";
import CampaignStatus from "../../../objects/enums/CampaignStatus";
import { CampaignMobileViewModel } from "../../../objects/viewmodels/Campaigns/Mobile/CampaignMobileViewModel";
import { IssuerViewModel } from "../../../objects/viewmodels/Users/issuers/IssuerViewModel";
import { paletteGrayLight, paletteGreen, paletteGreenBold, paletteRed } from "../../../utils/color";
import endPont from "../../../utils/endPoints";


export default function useCampaignDetaillPage(props: StackScreenProps<ParamListBase>) {
    const scrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);
    const [scrollToTopShowed, setScrollToTopShowed] = useState(false);
    const [issuerModalVisible, setIssuerModalVisible] = useState(false);

    const [campaign, setCampaign] = useState<CampaignMobileViewModel>();
    const [issuerDetail, setIssuerDetail] = useState<IssuerViewModel>();

    const getColor = () => {
        switch (campaign?.status) {
            case CampaignStatus.notStarted:
                return paletteGrayLight;
            case CampaignStatus.start:
                return paletteGreen;
            case CampaignStatus.end:
                return paletteGrayLight;
            case CampaignStatus.postpone:
                return paletteRed;
            default: return undefined;
        };
    }
    const getTextColor = () => {
        switch (campaign?.status) {
            case CampaignStatus.notStarted:
                return "black";
            case CampaignStatus.start:
                return paletteGreenBold;
            case CampaignStatus.end:
                return "black";
            case CampaignStatus.postpone:
                return paletteRed;
            default: return undefined;
        };
    }

    useEffect(() => {
        props.navigation.setOptions({ headerRight: (props) => createElement(CardHeader) });
        const params = props.route.params as { campaignId: number };
        //console.log(params);
        setLoading(true);
        console.log(params);
        appxios.get<CampaignMobileViewModel>(`${endPont.public.campaigns.customer.index}/${params.campaignId}`)
            .then(response => {
                console.log(response.status);
                setCampaign(response.data);
                props.navigation.setOptions({ title: response.data.name });
            })
            .finally(() => {
                setLoading(false);
            });
    }, []);

    const scrollToTop = () => {

        if (scrollViewRef.current) {
            scrollViewRef.current.scrollTo({ y: 0 });
            setScrollToTopShowed(false);
        }
    }

    const onScrollViewScroll = (e: NativeSyntheticEvent<NativeScrollEvent>) => {
        if (e.nativeEvent.contentOffset.y) {
            if (!scrollToTopShowed) {
                setScrollToTopShowed(true);
            }
        }
        else {
            if (scrollToTopShowed) {
                setScrollToTopShowed(false);
            }
        }
    }

    const onIssuerDetailPress = (issuer: IssuerViewModel) => {
        setIssuerDetail(issuer);
        setIssuerModalVisible(true);
    }


    return {
        ref: {
            scrollViewRef,
        },
        event: {
            onScrollViewScroll,
            onIssuerDetailPress
        },
        ui: {
            loading,
            getColor,
            getTextColor,
            issuerModalVisible: {
                value: issuerModalVisible,
                set: setIssuerModalVisible
            }
        },
        data: {
            campaign,
            issuerDetail
        },
        scrollToTop: {
            scrollToTopShowed,
            scrollToTop,
        }
    };
}