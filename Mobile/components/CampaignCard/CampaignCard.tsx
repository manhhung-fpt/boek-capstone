import React from 'react'
import { Text } from "@react-native-material/core";
import { View, Image, TouchableOpacity } from 'react-native';
import LinearGradient from 'react-native-linear-gradient'
import useRouter from '../../libs/hook/useRouter'
import { paletteGray, paletteGreenBold, paletteGreenShade1 } from '../../utils/color';
import locationWhite from "../../assets/icons/location-white.png";
import eventBusyWhite from "../../assets/icons/event-busy-white.png";
import truncateString from '../../libs/functions/truncateString';
import moment from 'moment';
import { CampaignViewModel } from '../../objects/viewmodels/Campaigns/CampaignViewModel';
import { dateTimeFormat } from '../../utils/format';
import Shadow from '../Shadow/Shadow';
import CampaignStatus from '../../objects/enums/CampaignStatus';
interface CampaignCardProps {
    campaign: CampaignViewModel;
}
function CampaignCard(props: CampaignCardProps) {
    const { push } = useRouter();
    return (
        <View
            style={{ alignItems: "center" }}>
            <View
                style={{ width: "90%" }}>
                <View style={{
                    //borderWidth: 1,
                    alignItems: "center",
                    height: "100%",
                    width: "100%"
                }}>
                    <Image source={{ uri: props.campaign.imageUrl }} style={{ height: "100%", width: "100%" }} resizeMethod="resize" resizeMode="contain" />
                </View>
            </View>
            <LinearGradient
                start={{ x: 0.5, y: 0.2 }}
                end={{ x: 0.5, y: 1 }}
                colors={['rgba(0,0,0,0)', 'rgba(0,0,0,1)']}
                style={{ position: "absolute", height: "100%", width: "100%" }}>
            </LinearGradient>
            <TouchableOpacity
                onPress={() => push("CampaignDetail", { campaignId: props.campaign.id })}
                style={{
                    position: "absolute",
                    width: "100%",
                    height: "100%",
                    alignItems: "center",
                    justifyContent: "center",
                }}>
                <View style={{
                    //borderWidth: 1,
                    width: "95%",
                    height: "90%",
                }}>
                    <View style={{
                        //borderWidth: 1,
                        height: "40%",
                        alignItems: "flex-end"
                    }}>
                        <Shadow style={{
                            borderRadius: 24,
                            backgroundColor:
                                props.campaign.status == 1 ? paletteGray
                                    :
                                    paletteGreenShade1,
                            paddingLeft: 10,
                            paddingRight: 10,
                            paddingTop: 5,
                            paddingBottom: 5,
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <Text style={{ color: "white" }}>{CampaignStatus.toString(props.campaign.status)}</Text>
                        </Shadow>
                    </View>
                    <View style={{
                        //borderWidth: 1,
                        height: "60%",
                        justifyContent: "flex-end"
                    }}>
                        <Text style={{
                            color: "white",
                            fontSize: 22,
                            fontWeight: "500"
                        }}>{truncateString(props.campaign.name, 6)}</Text>
                        <View style={{
                            //borderWidth : 1,
                            width: "100%",
                            flexDirection: "row"
                        }}>
                            <View style={{ width: "100%" }}>
                                {
                                    props.campaign.address &&
                                    <Text style={{ color: "white", marginLeft: 2 }}><Image source={locationWhite} resizeMode="contain" style={{ width: 17, height: 17 }} /> {props.campaign.address && props.campaign.address.length > 20 ? truncateString(props.campaign.address, 7) : props.campaign.address}</Text>
                                }
                                <Text style={{ color: "white", fontSize: 12, marginLeft: 2 }}>
                                    <Image source={eventBusyWhite} resizeMode="contain" style={{ width: 17, height: 17 }} />
                                    <Text style={{ color: "white", fontWeight: "500" }}> {moment(props.campaign.endDate).format(dateTimeFormat)}</Text>
                                </Text>
                            </View>
                            {/* <View style={{ width: "50%" }}>
                        <Text style={{ color: "white", textAlign: "right" }}><Image source={corporateFareBlack} resizeMode="contain" style={{ width: 17, height: 17 }} /> {item.campaignOrganizations.map(c => c.organization.name).join(", ")}</Text>
                        <Text style={{ color: "white", textAlign: "right" }}><Image source={theaterComedyBlack} resizeMode="contain" style={{ width: 17, height: 17 }} /> {item.participants.map(c => c.issuer.user.name).join(", ")}</Text>
                    </View> */}
                        </View>
                    </View>
                </View>
            </TouchableOpacity>
        </View>
    )
}

export default CampaignCard