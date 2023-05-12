import React from 'react'
import { TouchableOpacity, View, Image } from 'react-native';
import { Text } from "@react-native-material/core";
import LinearGradient from 'react-native-linear-gradient';
import { primaryTint6 } from '../../utils/color';
import useRouter from '../../libs/hook/useRouter';
import { CampaignViewModel } from '../../objects/viewmodels/Campaigns/CampaignViewModel';
import moment from 'moment';
import { dateTimeFormat } from '../../utils/format';
import locationWhite from "../../assets/icons/location-white.png";
import calendarTodayWhite from "../../assets/icons/calendar-today-white.png";
import corporateFareBlack from "../../assets/icons/corporate-fare-black.png";
import theaterComedyBlack from "../../assets/icons/theater-comedy-black.png";
import truncateString from '../../libs/functions/truncateString';
import DelimiterLine from '../DelimiterLine/DelimiterLine';
import Place from '../../assets/SvgComponents/Place';
import CampaignCard from '../CampaignCard/CampaignCard';

interface UpcomingBookFairProps {
    campaign: CampaignViewModel;
}
function UpcomingBookFair(props: UpcomingBookFairProps) {
    const { push } = useRouter();
    return (
        <View style={{
            height: 225,
            marginBottom: 15
        }}>
            <CampaignCard campaign={props.campaign} />
        </View>
    )
}

export default UpcomingBookFair




{/* <TouchableOpacity
onPress={() => push("CampaignDetail", { campaignId: props.campaign.id })}
style={{ width: "100%", height: "100%" }}>
<Image
    source={{ uri: props.campaign.imageUrl }}
    style={{ flex: 1, width: "100%", height: "100%" }}
    resizeMethod="resize"
    resizeMode="stretch" />
<LinearGradient
    start={{ x: 0.5, y: -0.1 }}
    end={{ x: 0.5, y: 1 }}
    colors={['rgba(0,0,0,0)', 'rgba(0,0,0,1)']}
    style={{ position: "absolute", height: "100%", width: "100%" }}>
</LinearGradient>
<View
    style={{ position: "absolute", width: "100%", height: "100%", alignItems: "center", justifyContent: "center", zIndex: 1 }}>
    <View style={{ width: "92%", height: "90%", justifyContent: "flex-end" }}>
        <Text style={{ color: "white", fontSize: 22, fontWeight: "500" }}>{props.campaign.name}</Text>
        <View style={{ width: "100%", flexDirection: "row" }}>
            <View style={{ width: "100%" }}>
                <Text style={{ color: "white" }} >
                    <Image source={locationWhite} resizeMode="contain" style={{ width: 17, height: 17 }} />
                    {props.campaign.address && props.campaign.address.length > 20 ? truncateString(props.campaign.address, 7) : props.campaign.address}
                </Text>
                <Text style={{ fontSize: 12 }} >
                    <Image source={calendarTodayWhite} resizeMode="contain" style={{ width: 17, height: 17 }} />
                    <Text style={{ color: "white", fontWeight: "500" }}> {moment(props.campaign.startDate).format(dateTimeFormat)}</Text>
                </Text>
            </View>
            <View style={{ width: "45%" }}>
            <Text style={{ textAlign: "right" }} >
                <Image source={corporateFareBlack} resizeMode="contain" style={{ width: 17, height: 17 }} /> {props.campaign.campaignOrganizations.map(item => item.organization.name).join(", ")}
            </Text>
            <Text style={{ textAlign: "right" }}><Image source={theaterComedyBlack} resizeMode="contain" style={{ width: 17, height: 17 }} /> {props.campaign.participants.map(item => item.issuer.user.name).join(", ")}</Text>
        </View> 
        </View>
    </View>
</View>
</TouchableOpacity> */}