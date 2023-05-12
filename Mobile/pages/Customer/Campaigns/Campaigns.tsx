import React, { useEffect } from 'react'
import { View, ScrollView, SafeAreaView, RefreshControl, TouchableOpacity, Image } from 'react-native'
import Swiper from 'react-native-swiper';
import { Text } from "@react-native-material/core";
import useCampaignsPage from './Campaigns.hook';
import UpcomingBookFair from '../../../components/UpcomingBookFair/UpcomingBookFair';
import PageLoader from '../../../components/PageLoader/PageLoader';
import PreLoadTabView from '../../../components/PreLoadTabView/PreLoadTabView';
import ShowMoreButton from '../../../components/ShowMoreButton/ShowMoreButton';
import { ParamListBase } from '@react-navigation/native';
import { BottomTabScreenProps } from '@react-navigation/bottom-tabs';
import LinearGradient from 'react-native-linear-gradient';
import corporateFareBlack from "../../../assets/icons/corporate-fare-black.png";
import theaterComedyBlack from "../../../assets/icons/theater-comedy-black.png";
import useRouter from '../../../libs/hook/useRouter';
import moment from 'moment';
import { dateTimeFormat } from '../../../utils/format';
import { paletteGray, paletteGreen, paletteGreenBold, paletteGreenShade1, primaryTint10, primaryTint8, primaryTint9 } from '../../../utils/color';
import Shadow from '../../../components/Shadow/Shadow';
import truncateString from '../../../libs/functions/truncateString';
import CampaignCard from '../../../components/CampaignCard/CampaignCard';
import DelimiterLine from '../../../components/DelimiterLine/DelimiterLine';
export interface CampaignsProps extends BottomTabScreenProps<ParamListBase> {

}
function Campaigns(props: CampaignsProps) {
    const hook = useCampaignsPage(props);
    const { push } = useRouter();
    return (
        <>
            <PageLoader loading={hook.loading} />
            <ScrollView
                refreshControl={
                    <RefreshControl refreshing={hook.scrollViewRefresh.refreshing}
                        onRefresh={hook.scrollViewRefresh.onRefresh} />}>
                <SafeAreaView
                    style={{
                        padding: 15,
                        backgroundColor: primaryTint10
                    }}>
                    {/* {
                        hook.data.onGoingCampagins &&
                        <View style={{
                            borderRadius: 8,
                            marginBottom: 15,
                            backgroundColor: "white",
                            shadowColor: "#000",
                            shadowOffset: {
                                width: 0,
                                height: 12,
                            },
                            shadowOpacity: 0.58,
                            shadowRadius: 16.00,
                            elevation: 24
                        }}>
                            <View style={{ width: "100%", padding: 10, marginBottom: 20 }}>
                                <Text variant='h6'>{hook.data.onGoingCampagins.title}</Text>
                            </View>
                            <View style={{ height: 225 }}>
                                <Swiper
                                    autoplay
                                    autoplayTimeout={8}
                                    showsButtons>
                                    {
                                        hook.data.onGoingCampagins.campaigns.map(item =>
                                            <CampaignCard campaign={item} />
                                        )
                                    }
                                </Swiper>
                            </View>
                        </View>
                    } */}
                    {/* <View style={{ borderWidth: 0.8, borderColor: paletteGray, marginBottom: 15 }}></View> */}

                    {
                        hook.data.upCampaginsContainer?.unhierarchicalCustomerCampaigns.map(item =>
                            <>
                                <View style={{
                                    backgroundColor: "white",
                                    padding: 12,
                                    borderRadius: 12,
                                    shadowColor: "#000",
                                    shadowOffset: {
                                        width: 0,
                                        height: 12,
                                    },
                                    shadowOpacity: 0.58,
                                    shadowRadius: 16.00,
                                    elevation: 6
                                }}>
                                    <View style={{ width: "100%", marginBottom: 15 }}>
                                        <Text variant='h6'>{item.title}</Text>
                                    </View>
                                    {
                                        item.campaigns.map((c, index) =>
                                            <>
                                                <UpcomingBookFair campaign={c} />
                                                {
                                                    index + 1 < item.campaigns.length &&
                                                    <DelimiterLine />
                                                }
                                            </>
                                        )
                                    }

                                </View>
                                <View style={{
                                    marginBottom: 20
                                }}>
                                    <ShowMoreButton onPress={() => { props.navigation.jumpTo("Search", { tab: "BookFairs" }) }} />
                                </View>
                            </>
                        )
                    }
                    {
                        hook.data.upCampaginsContainer?.hierarchicalCustomerCampaigns.map(item =>
                            <>
                                <View style={{
                                    backgroundColor: "white",
                                    padding: 12,
                                    borderRadius: 12,
                                    shadowColor: "#000",
                                    shadowOffset: {
                                        width: 0,
                                        height: 12,
                                    },
                                    shadowOpacity: 0.58,
                                    shadowRadius: 16.00,
                                    elevation: 6
                                }}>
                                    <View style={{ width: "100%", marginBottom: 15 }}>
                                        <Text variant='h6'>{item.title}</Text>
                                    </View>
                                    <PreLoadTabView
                                        titles={item.subHierarchicalCustomerCampaigns.map(item => item.subTitle)}
                                        childrens={item.subHierarchicalCustomerCampaigns.map(sub =>
                                            sub.campaigns.map((c,index) =>
                                                <>
                                                    <UpcomingBookFair campaign={c} />
                                                    {
                                                        index + 1 < sub.campaigns.length &&
                                                        <DelimiterLine />
                                                    }
                                                </>
                                            )
                                        )} />
                                </View>
                                <View style={{
                                    marginBottom: 20
                                }}>
                                    <ShowMoreButton onPress={() => { props.navigation.jumpTo("Search", { tab: "BookFairs" }) }} />
                                </View>
                            </>
                        )
                    }

                </SafeAreaView>
            </ScrollView>
        </>
    )
}

export default Campaigns