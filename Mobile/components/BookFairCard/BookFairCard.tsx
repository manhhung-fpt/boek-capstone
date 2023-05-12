import React from 'react'
import { View, Image, Text, Dimensions, TouchableOpacity } from 'react-native'
import { paletteGrayLight, paletteGreen, paletteGreenBold, paletteRed, primaryTint4 } from '../../utils/color';
import locationBlack from "../../assets/icons/location-black.png";
import calendarBlack from "../../assets/icons/calendar-today-black.png";
import useRouter from '../../libs/hook/useRouter';
import { CampaignViewModel } from '../../objects/viewmodels/Campaigns/CampaignViewModel';
import moment from 'moment';
import CampaignStatus from '../../objects/enums/CampaignStatus';
interface BookFairCardProps {
    campagin?: CampaignViewModel
}

function BookFairCard({ campagin }: BookFairCardProps) {
    const { push } = useRouter();
    const getColor = () => {
        switch (campagin?.status) {
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
        switch (campagin?.status) {
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

    return (
        <TouchableOpacity
            onPress={() => push("CampaignDetail", { campaignId: campagin?.id })}
            style={{
                //borderWidth: 1,
                //borderColor: primaryTint4,
                //borderRadius: 8,
                padding: 10,
                flexDirection: "row",
                flexWrap: "wrap"
            }}>
            <View style={{ flexDirection: "row" }}>
                <View style={{ marginBottom: 5, width: "45%" }}>
                    <Image
                        source={{ uri: campagin?.imageUrl }}
                        resizeMode="contain"
                        style={{
                            width: "100%",
                            height: (9 / 16) * ((Dimensions.get("screen").width - 20) * 45 / 100)
                        }} />
                </View>
                <View style={{ padding: 10, width: "55%" }}>
                    <Text style={{ fontSize: 18, fontWeight: "600", marginBottom: 5 }}>{campagin?.name}</Text>
                    <View style={{ flexDirection: "row" }}>
                        <View style={{ alignItems: "center", justifyContent: "center" }}>
                            <Image source={calendarBlack} style={{ height: 17, width: 25 }} resizeMode="contain" />
                        </View>

                        <Text>{moment(campagin?.startDate).format("DD/MM/YYYY HH:MM:SS")}</Text>
                    </View>
                    <View style={{ flexDirection: "row" }}>
                        <View style={{ alignItems: "center", justifyContent: "center" }}>
                            <Image source={locationBlack} style={{ height: 17, width: 25 }} resizeMode="contain" />
                        </View>
                        <Text>{"Địa điểm"}</Text>
                    </View>
                </View>
            </View>
            <View style={{ width: "100%", flexDirection: "row" }}>
                <View style={{ width: "100%", flexDirection: "row", alignItems: "center" }}>
                    {
                        campagin?.participants.map((item, index) =>
                            <View
                                style={{
                                    marginLeft: 15 * index,
                                    position: index == campagin.participants.length - 1 ? "relative" : "absolute",
                                    borderRadius: 999,
                                    width: 25,
                                    height: 25,
                                    overflow: "hidden"
                                }}>
                                <Image source={{ uri: item.issuer.user.imageUrl }} style={{ width: "100%", height: "100%" }} />
                            </View>
                        )
                    }
                    <View style={{ paddingLeft: 5, flex: 1 }}>
                        <Text >Nhà phát hành: {campagin?.participants.map(item => item.issuer.user.name).join(", ")}</Text>
                    </View>
                </View>
            </View>
            <View style={{
                backgroundColor: getColor(),
                marginTop: 10,
                marginBottom: 10,
                alignItems: "center",
                justifyContent: "center",
                width: "30%",
                height: 25,
                borderRadius: 24
            }}>
                <Text style={{
                    color: getTextColor(),
                    fontSize: 13,
                    fontWeight: "500"
                }}>{CampaignStatus.toString(campagin?.status as number)}</Text>
            </View>
        </TouchableOpacity >
    )
}

export default BookFairCard