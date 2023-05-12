import React from 'react'
import { ScrollView, View, Text, Image, TouchableOpacity, ActivityIndicator, Pressable } from 'react-native'
import { paletteGrayTint8, primaryColor, primaryTint1, primaryTint5, primaryTint6, } from '../../../utils/color';
import moment from 'moment';
import useStaffCampaignsPage from './StaffCampaigns.hook';
import PageLoader from '../../../components/PageLoader/PageLoader';
import useRouter from '../../../libs/hook/useRouter';
import { createMaterialTopTabNavigator, MaterialTopTabScreenProps } from '@react-navigation/material-top-tabs';
import Paging from '../../../components/Paging/Paging';
import { ParamListBase } from '@react-navigation/native';
import PreLoadTabView from '../../../components/PreLoadTabView/PreLoadTabView';
import UpcomingBookFair from '../../../components/UpcomingBookFair/UpcomingBookFair';
import Shadow from '../../../components/Shadow/Shadow';
import StaffCampaignExpandView from '../../../components/StaffCampaignExpandView/StaffCampaignExpandView';
import { MobileCampaignStaffsViewModel } from '../../../objects/viewmodels/CampaignStaff/MobileCampaignStaffsViewModel';
import LayoutModal from '../../../components/LayoutModal/LayoutModal';
import BouncyCheckbox from 'react-native-bouncy-checkbox';
import { Button } from '@rneui/base';
import { StaffCampaignMobilesViewModel } from '../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel';

const Tab = createMaterialTopTabNavigator();
function StaffCampaigns() {
    const dateTimeFormat = "DD/MM/YYYY";
    const { navigate } = useRouter();
    const hook = useStaffCampaignsPage();
    return (
        <>
            <LayoutModal
                visible={hook.ui.chooseSchedueModalVisible}
                onClose={hook.event.onChooseSchedueModalVisibleClose}>
                <Pressable
                    onPress={hook.event.onChooseSchedueModalVisibleClose}
                    style={{
                        width: "100%",
                        height: "100%",
                        alignItems: "center",
                        justifyContent: "center",
                        backgroundColor: "rgba(0,0,0,0.6)"
                    }}>
                    <View style={{
                        width: "80%",
                        backgroundColor: "white",
                        borderRadius: 8
                    }}>
                        <ScrollView style={{
                            maxHeight: "80%",
                            padding: 20
                        }}>
                            {
                                hook.data.schedueSelect.map(item =>
                                    <View style={{
                                        flexDirection: "row"
                                    }}>

                                        <BouncyCheckbox
                                            textComponent={
                                                <View style={{
                                                    rowGap: 5,
                                                    marginLeft: 10
                                                }}>
                                                    <Text style={{ fontSize: 15 }}>Địa điểm: {item.address}</Text>
                                                    <Text style={{ fontSize: 15 }}>Thời gian: {moment(item.startDate).format(dateTimeFormat)}</Text>
                                                </View>
                                            }
                                            isChecked={hook.input.selectedCampaign.value?.selectedSchedule?.id == item.id}
                                            onPress={() => hook
                                                .input
                                                .selectedCampaign
                                                .set({ ...hook.input.selectedCampaign.value as StaffCampaignMobilesViewModel, selectedSchedule: item })}
                                            disableBuiltInState
                                            fillColor={primaryTint1}
                                        />
                                    </View>
                                )
                            }
                        </ScrollView>
                        <View style={{
                            display: hook.input.selectedCampaign.value?.selectedSchedule ? "flex" : "none",
                            padding: 20
                        }}>
                            <Button
                                onPress={() => hook.event.onCampaignPress()}
                                buttonStyle={{
                                    backgroundColor: primaryColor
                                }}>Tiếp theo</Button>
                        </View>
                    </View>
                </Pressable>
            </LayoutModal>
            <PageLoader loading={hook.ui.loading} />
            <ScrollView
                ref={hook.ref.scrollViewRef}
                style={{ backgroundColor: "white" }}>
                <View style={{ padding: 15, paddingBottom: 30, height: "100%" }}>
                    <Shadow style={{
                        padding: 15,
                        paddingBottom: 30,
                        backgroundColor: "white",
                        borderRadius: 8,
                        height: "100%"
                    }}>
                        <PreLoadTabView
                            titles={hook.data.campagins.map(item => item.title)}
                            childrens={hook.data.campagins.map(sub =>
                                sub.campaigns.map(c =>
                                    <StaffCampaignExpandView
                                        disabled={sub.title == "Hội sách kết thúc"}
                                        backgroundColor={sub.title == "Hội sách kết thúc" ? paletteGrayTint8 : "white"}
                                        onPress={() => hook.event.onCampaignPress(c)}
                                        campaign={c} />
                                )
                            )} />
                    </Shadow>
                </View>
            </ScrollView>
        </>
    )
}


export default StaffCampaigns