import { View, Image, Dimensions, FlatList, TouchableOpacity, ScrollView } from 'react-native'
import { Text } from "@react-native-material/core";
import img1 from "../../../assets/wtd.webp";
import locationBlack from "../../../assets/icons/location-black.png";
import calendarBlack from "../../../assets/icons/calendar-today-black.png";
import avatar from "../../../assets/avatar.jpg";
import useCampaignDetaillPage from './CampaignDetail.hook';
import verticalAlignTopWhite from "../../../assets/icons/vertical-align-top-white.png";
import navigateRightWhite from "../../../assets/icons/navigate-right-white.png";
import useRouter from '../../../libs/hook/useRouter';
import LabeledImage from '../../../components/LabeledImage/LabeledImage';
import TitleFlatBooks from '../../../components/TitleFlatBooks/TitleFlatBooks';
import ShowMoreButton from '../../../components/ShowMoreButton/ShowMoreButton';
import { paletteGray, paletteGrayLight, paletteGrayTint6, paletteGrayTint9, paletteGreen, paletteGreenBold, paletteRed, primaryColor, primaryTint1, primaryTint2, primaryTint7, primaryTint8, primaryTint9 } from '../../../utils/color';
import FadeTransition from '../../../components/FadeTransition/FadeTransition';
import eventBusyBlack from "../../../assets/icons/event-busy-black.png";
import LayoutModal from '../../../components/LayoutModal/LayoutModal';
import { StackScreenProps } from '@react-navigation/stack';
import { ParamListBase } from '@react-navigation/native';
import PageLoader from '../../../components/PageLoader/PageLoader';
import { dateTimeFormat } from '../../../utils/format';
import moment from 'moment';
import TitleTabedFlatBooks from '../../../components/TitleTabedFlatBooks/TitleTabedFlatBooks';
import CampaignStatus from '../../../objects/enums/CampaignStatus';
import { Button } from '@rneui/base';
import Shadow from '../../../components/Shadow/Shadow';

function CampaignDetail(props: StackScreenProps<ParamListBase>) {
    const hook = useCampaignDetaillPage(props);
    const { push } = useRouter();

    return (
        <>
            <PageLoader loading={hook.ui.loading} opacity={1} />

            <LayoutModal
                visible={hook.ui.issuerModalVisible.value}
                onClose={() => hook.ui.issuerModalVisible.set(false)}
                closeOverlay>
                <View style={{
                    //borderWidth: 1,
                    width: "100%",
                    height: "100%",
                    alignItems: "center",
                    justifyContent: "center",
                    backgroundColor: "rgba(0,0,0,0.5)"
                }}>
                    <View
                        style={{
                            backgroundColor: paletteGrayTint9,
                            borderRadius: 8,
                            shadowColor: "#000",
                            shadowOffset: {
                                width: 0,
                                height: 12,
                            },
                            shadowOpacity: 0.58,
                            shadowRadius: 16.00,
                            elevation: 24,
                            height: 240,
                            width: "95%",
                            flexDirection: "row"
                        }}>
                        <View style={{
                            width: "30%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <View style={{ borderRadius: 999, overflow: "hidden" }}>
                                <Image
                                    source={{ uri: hook.data.issuerDetail?.user.imageUrl }}
                                    resizeMode="cover"
                                    style={{
                                        width: 90,
                                        height: 90
                                    }} />
                            </View>
                        </View>
                        <View style={{
                            width: "70%",
                            justifyContent: "center"
                        }}>
                            <Text style={{ fontSize: 16, margin: 5 }}><Text style={{ fontWeight: "600" }}>Tên: </Text>{hook.data.issuerDetail?.user.name}</Text>
                            <Text style={{ fontSize: 16, margin: 5 }}><Text style={{ fontWeight: "600" }}>SĐT: </Text>{hook.data.issuerDetail?.user.phone}</Text>
                            <Text style={{ fontSize: 16, margin: 5 }}><Text style={{ fontWeight: "600" }}>Địa chỉ: </Text>{hook.data.issuerDetail?.user.address}</Text>
                            <Text style={{ fontSize: 16, margin: 5 }}><Text style={{ fontWeight: "600" }}>Email: </Text>{hook.data.issuerDetail?.user.email}</Text>
                        </View>
                    </View>


                </View>
            </LayoutModal>

            <FadeTransition
                showed={hook.scrollToTop.scrollToTopShowed}
                style={{
                    backgroundColor: primaryTint1,
                    width: 40,
                    height: 40,
                    borderRadius: 999,
                    position: "absolute",
                    zIndex: 1,
                    bottom: 60,
                    right: 10,
                    justifyContent: "center",
                    alignItems: "center"
                }}>
                <TouchableOpacity
                    onPress={hook.scrollToTop.scrollToTop}
                    style={{

                    }}>
                    <Image
                        source={verticalAlignTopWhite}
                        resizeMode="cover"
                        style={{ width: 24, height: 24 }} />
                </TouchableOpacity>
            </FadeTransition>

            <ScrollView
                onScrollEndDrag={hook.event.onScrollViewScroll}
                onMomentumScrollEnd={hook.event.onScrollViewScroll}
                ref={hook.ref.scrollViewRef}>
                <View style={{ backgroundColor: "white", padding: 15 }}>
                    <Shadow style={{
                        backgroundColor: "white",
                        borderRadius: 8,
                        padding: 10
                    }}>
                        <Text variant="h5" style={{ marginBottom: 10 }} >{hook.data.campaign?.name}</Text>
                        <View style={{ marginTop: 4, backgroundColor: hook.ui.getColor(), alignItems: "center", justifyContent: "center", width: "40%", height: 25, borderRadius: 24 }}>
                            <Text style={{ fontSize: 13, fontWeight: "500", color: hook.ui.getTextColor() }}>{CampaignStatus.toString(hook.data.campaign?.status as number)}</Text>
                        </View>
                        <View style={{ width: "100%", alignItems: "center" }}>
                            <Image source={{ uri: hook.data.campaign?.imageUrl }} style={{ marginTop: 20, marginBottom: 20, width: "90%", height: Dimensions.get("window").width / 16 * 9 }} resizeMethod="resize" resizeMode="contain" />
                        </View>
                        <View style={{ width: "95%", rowGap: 7 }}>
                            {
                                hook.data.campaign?.address &&
                                <View style={{ flexDirection: "row", marginBottom: 5 }}>
                                    <View style={{ alignItems: "center", justifyContent: "center", marginRight: 5 }}>
                                        <Image style={{ height: 20, width: 20 }} source={locationBlack} />
                                    </View>
                                    <Text>{hook.data.campaign?.address}</Text>
                                </View>
                            }
                            <View style={{ flexDirection: "row", marginBottom: 10 }}>
                                <View style={{ alignItems: "center", justifyContent: "center", marginRight: 5 }}>
                                    <Image style={{ height: 20, width: 20, marginRight: 2 }} source={calendarBlack} />
                                </View>
                                <Text >Bắt đầu : {moment(hook.data.campaign?.startDate).format(dateTimeFormat)}</Text>
                            </View>
                            <View style={{ flexDirection: "row", marginBottom: 10 }}>
                                <View style={{ alignItems: "center", justifyContent: "center", marginRight: 5 }}>
                                    <Image style={{ height: 20, width: 20, marginRight: 2 }} source={eventBusyBlack} />
                                </View>
                                <Text >Kết thúc : {moment(hook.data.campaign?.endDate).format(dateTimeFormat)}</Text>
                            </View>
                        </View>

                    </Shadow>

                    <Shadow style={{
                        backgroundColor: "white",
                        marginTop: 20,
                        borderRadius: 8,
                        padding: 10
                    }}>
                        <Text variant="h6" style={{ marginTop: 10, marginBottom: 10 }}>Nhà phát hành</Text>
                        {
                            hook.data.campaign?.issuers?.length == 0 || !hook.data.campaign?.issuers ?
                                <View>
                                    <Text style={{ fontSize: 17 }}>Chưa có nhà phát hành</Text>
                                </View>
                                :
                                <>
                                    <FlatList
                                        horizontal
                                        data={hook.data.campaign?.issuers}
                                        renderItem={e =>
                                            <View style={{ marginRight: 20 }}>
                                                <LabeledImage
                                                    onPress={() => hook.event.onIssuerDetailPress(e.item)}
                                                    label={e.item?.user.name} source={{ uri: e.item.user.imageUrl }} />
                                            </View>
                                        } />
                                </>
                        }
                    </Shadow>

                    <View style={{ marginTop: 20 }}>
                        {
                            hook.data.campaign?.unhierarchicalBookProducts?.map(item =>
                                <TitleFlatBooks
                                    title={item.title}
                                    data={item.bookProducts as any} />
                            )
                        }
                        {
                            hook.data.campaign?.hierarchicalBookProducts?.map(item =>
                                <>
                                    <TitleTabedFlatBooks
                                        title={item.title}
                                        data={item.subHierarchicalBookProducts?.map(product => ({ tabLabel: product.subTitle, tabData: product.bookProducts })) as any} />
                                    {/* <ShowMoreButton onPress={() => push("IssuerMoreBook")} /> */}
                                </>
                            )
                        }
                    </View>

                    <Shadow style={{
                        marginTop: 20,
                        borderRadius: 8,
                        padding: 10,
                        backgroundColor: "white"
                    }}>
                        <View style={{}}>
                            <Text variant="h6" style={{ marginTop: 10, marginBottom: 10 }}>Mô tả</Text>
                            <Text>{hook.data.campaign?.description}</Text>
                        </View>
                    </Shadow>

                    {
                        hook.data.campaign?.organizations &&
                        <Shadow style={{
                            elevation: 1,
                            marginTop: 20,
                            backgroundColor: "white",
                            padding: 10,
                            borderRadius: 8
                        }}>
                            <View style={{ width: "100%", flexDirection: "row" }}>
                                <View style={{ width: "85%" }}>
                                    <Text variant="h6" style={{ marginTop: 10, marginBottom: 10 }}>Tổ chức</Text>
                                </View>
                                {
                                    hook.data.campaign?.isRecurring &&
                                    <View style={{ width: "15%", alignItems: "center", justifyContent: "center" }}>
                                        <Button
                                            onPress={() => push("RecurringCampaign", { data: hook.data.campaign })}
                                            buttonStyle={{ backgroundColor: primaryTint2, borderRadius: 999, width: 30, height: 30 }}>
                                            <Image source={navigateRightWhite} style={{ width: 25, height: 25 }} resizeMode="contain" />
                                        </Button>
                                    </View>
                                }
                            </View>
                            <FlatList
                                horizontal
                                data={hook.data.campaign?.organizations}
                                renderItem={e =>
                                    <View style={{ marginRight: 20 }}>
                                        <LabeledImage label={e.item?.name} source={{ uri: e.item.imageUrl }} />
                                    </View>
                                } />
                        </Shadow>
                    }
                    {
                        hook.data.campaign?.groups &&
                        <Shadow style={{
                            elevation: 1,
                            marginTop: 20,
                            backgroundColor: "white",
                            padding: 10,
                            borderRadius: 8
                        }}>
                            <View style={{ width: "85%" }}>
                                <Text variant="h6" style={{ marginTop: 10, marginBottom: 10 }}>Nhóm</Text>
                            </View>
                            <FlatList
                                horizontal
                                data={hook.data.campaign?.groups}
                                renderItem={e =>
                                    <View style={{ marginRight: 20 }}>
                                        <View
                                            style={{
                                                height: 90,
                                                alignItems: "center",
                                                justifyContent: "center"
                                            }}>
                                            <View
                                                style={{
                                                    alignItems: "center",
                                                    justifyContent: "center"
                                                }}>
                                                <View
                                                    style={{
                                                        //borderWidth: 1,
                                                        //borderColor: paletteGrayLight,
                                                        width: 50,
                                                        height: 50,
                                                        borderRadius: 999,
                                                        overflow: "hidden",
                                                        alignItems: "center",
                                                        justifyContent: "center",
                                                        backgroundColor: paletteGrayTint6,
                                                        //marginBottom: 5,

                                                    }}>
                                                    <Text>{e.item.name[0]}</Text>
                                                </View>
                                                <View>
                                                    <Text style={{ fontSize: 15 }}>{e.item.name}</Text>
                                                </View>
                                            </View>
                                        </View>
                                    </View>
                                } />
                        </Shadow>
                    }
                    <View style={{ borderWidth: 1, borderColor: primaryTint7, borderRadius: 8, padding: 10, marginTop: 20, marginBottom: 10 }}>
                        <Text style={{ fontSize: 18, fontWeight: "600", }}>Lưu ý</Text>
                        <Text style={{ fontSize: 13, }}>Boek không chịu trách nhiệm về việc đơn hàng đổi trả sách của khách hàng. Xin liên hệ về các nhà phát hành nếu liên quan về đổi trả sách.</Text>
                    </View>

                </View>

            </ScrollView>
        </>
    );
}

export default CampaignDetail