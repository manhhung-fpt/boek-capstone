import React, { useEffect, useRef, useState } from 'react'
import { Animated, TouchableOpacity, View, Image, ColorValue } from 'react-native';
import { Text } from '@react-native-material/core'
import { paletteGrayLight, paletteGrayShade3, paletteGrayTint6, paletteGrayTint7, paletteGrayTint8, paletteGreenShade1, primaryTint1 } from '../../utils/color';
import expandMoreBlack from "../../assets/icons/expand-more-black.png"
import { Button, CheckBox } from '@rneui/base';
import { CampaignViewModel } from '../../objects/viewmodels/Campaigns/CampaignViewModel';
import { MobileCampaignStaffsViewModel } from '../../objects/viewmodels/CampaignStaff/MobileCampaignStaffsViewModel';
import { StaffCampaignMobilesViewModel } from '../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel';
import moment from 'moment';
import { dateFormat } from '../../utils/format';
import CampaignStatus from '../../objects/enums/CampaignStatus';
import truncateString from '../../libs/functions/truncateString';
interface StaffCampaignExpandView {
    backgroundColor: ColorValue;
    campaign: StaffCampaignMobilesViewModel;
    onPress?: () => void;
    disabled: boolean;
}
function StaffCampaignExpandView(props: StaffCampaignExpandView) {
    const [isExpanded, setIsExpanded] = useState(false);
    const animation = useRef(new Animated.Value(0)).current;
    const handleExpand = () => {
        setIsExpanded(!isExpanded);
    };
    useEffect(() => {
        Animated.timing(animation, {
            toValue: isExpanded ? 300 : 140,
            duration: 300,
            useNativeDriver: false,
        }).start();
    }, [isExpanded]);
    return (
        <Animated.View
            style={{
                //flex: 1,
                //borderWidth: 1,
                height: animation,
                marginBottom: 15,
            }}>
            <TouchableOpacity
                disabled={props.disabled}
                onPress={props.onPress}
                style={{
                    height: "100%",
                    padding: 15,
                    borderWidth: 0.5,
                    borderRadius: 8,
                    //flexDirection: "row",
                    backgroundColor: props.backgroundColor,
                    //height: "100%",

                }}>
                {/* {
                    props.campaign.isRecurring &&
                    <View style={{
                        position: "absolute",
                        borderRadius: 3,
                        backgroundColor: paletteGreenShade1,
                        alignItems: "center",
                        justifyContent: "center",
                        top: -5,
                        right: -5,
                        zIndex: 5
                    }}>
                        <Text style={{ color: "white", padding: 5 }}>Liên hoàn</Text>
                    </View>
                } */}
                <View style={{
                    flexDirection: "row",
                    height: 110,
                    //borderWidth: 1,
                }}>
                    <View style={{
                        width: "70%",
                        rowGap: 5
                    }}>
                        <Text>{truncateString(props.campaign.name, 3)}</Text>
                        <Text style={{ fontSize: 14, color: paletteGrayShade3 }}>Ngày bắt đầu: {moment(props.campaign.startDate).format(dateFormat)}</Text>
                        {
                            props.campaign.address &&
                            <Text style={{ fontSize: 14, color: paletteGrayShade3 }}>{truncateString(props.campaign.address, 8)}</Text>
                        }
                    </View>
                    <View style={{
                        width: "30%",
                        height: "100%"
                    }}>
                        <View
                            style={{
                                //borderWidth: 1,
                                height: "60%"
                            }}>
                            <Image source={{ uri: props.campaign.imageUrl }} resizeMode="contain" style={{ height: "100%" }} />
                        </View>
                        {
                            props.campaign.isRecurring &&
                            <View style={{
                                //borderWidth: 1,
                                height: "40%",
                                alignItems: "flex-end",
                                justifyContent: "flex-end"
                            }}>
                                <View
                                    style={{
                                        //borderWidth: 1,
                                        //backgroundColor: primaryTint1,
                                        borderRadius: 999,
                                        width: 25,
                                        height: 25,
                                        alignItems: "center",
                                        justifyContent: "center"
                                    }}>
                                    <Image source={expandMoreBlack} style={{ height: "100%", width: "100%" }} />
                                </View>
                            </View>
                        }
                    </View>
                </View>



            </TouchableOpacity>
        </Animated.View>
    )
}

export default StaffCampaignExpandView