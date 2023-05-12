import React from 'react'
import { ScrollView, TouchableOpacity, View, Image, Pressable } from 'react-native'
import { Text } from '@react-native-material/core'
import PageLoader from '../../../components/PageLoader/PageLoader'
import StickyHeaderSearchBar from '../../../components/StickyHeaderSearchBar/StickyHeaderSearchBar'
import { paletteGrayTint8, primaryColor, primaryTint1 } from '../../../utils/color'
import Shadow from '../../../components/Shadow/Shadow'
import range from '../../../libs/functions/range'
import Paging from '../../../components/Paging/Paging'
import { Button } from '@rneui/base'
import StaffCampaignExpandView from '../../../components/StaffCampaignExpandView/StaffCampaignExpandView'
import { MobileCampaignStaffsViewModel } from '../../../objects/viewmodels/CampaignStaff/MobileCampaignStaffsViewModel'
import useRouter from '../../../libs/hook/useRouter'
import { StaffCampaignMobilesViewModel } from '../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel'
import { StackScreenProps } from '@react-navigation/stack'
import { ParamListBase } from '@react-navigation/native'
import useCreateChooseCampaignOrderPage from './CreateChooseCampaignOrder.hook'
import StaffCampaigns from '../StaffCampaigns/StaffCampaigns'
import PreLoadTabView from '../../../components/PreLoadTabView/PreLoadTabView'
import LayoutModal from '../../../components/LayoutModal/LayoutModal'
import BouncyCheckbox from 'react-native-bouncy-checkbox'
import { dateTimeFormat } from '../../../utils/format'
import moment from 'moment'

function CreateChooseCampaignOrder(props: StackScreenProps<ParamListBase>) {
    const { push } = useRouter();
    const hook = useCreateChooseCampaignOrderPage(props);
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
                        height: "80%",
                        width: "80%",
                        backgroundColor: "white",
                        borderRadius: 8
                    }}>
                        <ScrollView style={{
                            height: "60%",
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
            <View>
                <Button
                    onPress={() => push("CreateChooseHaveAccountOrder")}
                    buttonStyle={{
                        backgroundColor: primaryTint1,
                        //height: "10%"
                    }}>Tiếp theo</Button>
            </View>
        </>
    )
}

export default CreateChooseCampaignOrder