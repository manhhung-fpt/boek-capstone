import { Button } from '@rneui/base'
import React from 'react'
import { Pressable, ScrollView, TouchableOpacity, View, Image } from 'react-native'
import { Text } from '@react-native-material/core'
import LayoutModal from '../../../components/LayoutModal/LayoutModal'
import { paletteGray, paletteGreenBold, paletteOrange, palettePink, primaryTint1, primaryTint4 } from '../../../utils/color'
import BarCode from '../../../assets/SvgComponents/BarCode'
import useRouter from '../../../libs/hook/useRouter'
import Place from '../../../assets/SvgComponents/Place'
import ExpandToggleView from '../../../components/ExpandToggleView/ExpandToggleView'
import formatNumber from '../../../libs/functions/formatNumber'
import { StackScreenProps } from '@react-navigation/stack'
import { ParamListBase } from '@react-navigation/native'
import useCreateConfirmOrderPage from './CreateConfirmOrder.hook'
import packageBlack from "../../../assets/icons/package-black.png";
import navigateRightBlack from "../../../assets/icons/navigate-right-black.png";
import BouncyCheckbox from 'react-native-bouncy-checkbox'
import useAppContext from '../../../context/Context'
import DelimiterLine from '../../../components/DelimiterLine/DelimiterLine'
import { OrderPayment } from '../../../objects/enums/OrderPayment'
import PageLoader from '../../../components/PageLoader/PageLoader'

function CreateConfirmOrder(props: StackScreenProps<ParamListBase>) {
    const { user, staffCart } = useAppContext();
    const { push } = useRouter();
    const hook = useCreateConfirmOrderPage(props);
    return (
        <>
            <PageLoader loading={hook.ui.loading} />

            <LayoutModal visible={hook.ui.infoModalVisible} onClose={() => hook.ui.setInfoModalVisible(!hook.ui.infoModalVisible)}>
                <Pressable
                    onPress={() => hook.ui.setInfoModalVisible(false)}
                    style={{
                        width: "100%",
                        height: "100%",
                        alignItems: "center",
                        justifyContent: "center",
                        backgroundColor: "rgba(0,0,0,0.6)"
                    }}>
                    <View style={{
                        backgroundColor: "white",
                        borderRadius: 24,
                        width: "95%",
                        height: "35%",
                        padding: 20,
                        shadowColor: "#000",
                        shadowOffset: {
                            width: 0,
                            height: 12,
                        },
                        shadowOpacity: 0.58,
                        shadowRadius: 16.00,
                        elevation: 24,
                        rowGap: 15,
                        justifyContent: "center"
                    }}>
                        <Text style={{ fontSize: 16 }}>
                            • NPH sẽ phụ trách giao hàng đến địa chỉ của
                            bạn.
                        </Text>
                        <Text style={{ fontSize: 16 }}>
                            • Phí vận chuyển của đơn phụ thuộc vào nội
                            thành hay ngoại thành đối với các nơi hội
                            sách đang tổ chức {"\n"}
                            o Nội thành: 15,000 đ {"\n"}
                            o Ngoại thành: 30,000 đ
                        </Text>
                        <Text style={{ fontSize: 16 }}>
                            • Lưu ý: Boek không chịu trách nhiệm về đơn
                            đổi trả của khách hàng. Xin liên hệ NPH về
                            vấn đề này.
                        </Text>
                    </View>
                </Pressable>
            </LayoutModal>

            <ScrollView
                style={{
                    backgroundColor: "white",
                    padding: 10
                }}>
                <View
                    style={{
                        padding: 10,
                        flexDirection: "row"
                    }}>
                    <View style={{
                        //borderWidth: 1,
                        width: "15%",
                    }}>
                        <Image source={packageBlack} style={{ width: 35, height: 35 }} />
                    </View>
                    <View style={{
                        //borderWidth: 1,
                        width: "75%",
                        rowGap: 5
                    }}>
                        {
                            hook.data.params.customer?.name &&
                            <Text style={{ fontSize: 16 }}>Tên: {hook.data.params.customer.name}</Text>
                        }
                        {
                            hook.data.params.customer?.phone &&
                            <Text style={{}}>SĐT: {hook.data.params.customer?.phone}</Text>
                        }
                        {
                            hook.data.params.customer?.email &&
                            <Text style={{}}>Địa chỉ: {hook.data.params.customer?.email}</Text>
                        }
                        {
                            <Text style={{}}>Nhân viên: {user?.name}</Text>
                        }
                    </View>
                </View>
                <DelimiterLine />
                <View style={{
                    marginBottom: 15,
                    overflow: "hidden",
                    borderWidth: 1,
                    borderColor: primaryTint4,
                    borderRadius: 8
                }}>
                    <ExpandToggleView initExpanded label={hook.data.params.campaign.name || ""}>
                        <View style={{ padding: 10, borderTopWidth: 1, borderTopColor: primaryTint4 }}>
                            {
                                staffCart.map(product =>
                                    <>
                                        <Text style={{ fontSize: 16, marginBottom: 10, color: paletteGray }}>{product.issuerName}</Text>
                                        <View style={{
                                            flexDirection: "row",
                                            marginBottom: 20
                                        }}>
                                            <View style={{
                                                borderWidth: 1,
                                                borderColor: primaryTint1,
                                                width: "20%",
                                                height: 100,
                                                borderRadius: 8
                                            }}>
                                                <Image resizeMode='contain' style={{ width: "100%", height: "100%" }} source={{ uri: product.imageUrl }} />
                                            </View>
                                            <View style={{
                                                width: "50%",
                                                paddingLeft: 10,
                                                justifyContent: "center"
                                            }}>
                                                <Text style={{ fontSize: 16 }}>{product.title}</Text>
                                                <Text>Số lượng: x{product.quantity}</Text>
                                            </View>
                                            <View style={{ width: "30%", alignItems: "flex-end", justifyContent: "center" }}>
                                                <Text style={{ color: palettePink, fontSize: 18, fontWeight: "700" }}>{formatNumber(product.salePrice)}đ</Text>
                                            </View>
                                        </View>
                                    </>
                                )
                            }
                        </View>
                    </ExpandToggleView>
                </View>
                <DelimiterLine />
                <View style={{
                    rowGap: 15,
                    padding: 10
                }}>
                    <Text style={{ fontSize: 16 }}>Phương thức thanh toán</Text>
                    <BouncyCheckbox
                        isChecked={hook.input.paymentMethod.value == OrderPayment.Cash}
                        onPress={() => hook.input.paymentMethod.set(OrderPayment.Cash)}
                        disableBuiltInState
                        size={20}
                        fillColor={primaryTint1}
                        text="Tiền mặt" textStyle={{ textDecorationLine: "none" }} />
                    {/* <BouncyCheckbox
                        isChecked={hook.input.paymentMethod.value == OrderPayment.ZaloPay}
                        onPress={() => hook.input.paymentMethod.set(OrderPayment.ZaloPay)}
                        disableBuiltInState
                        size={20}
                        fillColor={primaryTint1}
                        text="ZaloPay" textStyle={{ textDecorationLine: "none" }} /> */}
                </View>
                <DelimiterLine />
                <View style={{
                    rowGap: 5,
                    padding: 10
                }}>
                    <View style={{ flexDirection: "row" }}>
                        <View style={{ width: "50%" }}>
                            <Text style={{ fontSize: 16 }}>Tạm tính</Text>
                        </View>
                        <View style={{ width: "50%", alignItems: "flex-end" }}>
                            <Text style={{ fontSize: 16 }}>{formatNumber(hook.data.calculation?.subTotal)}đ</Text>
                        </View>
                    </View>
                    {/* <View style={{ flexDirection: "row" }}>
                        <View style={{ width: "50%" }}>
                            <Text style={{ fontSize: 16, color: palettePink }}>Tích điểm</Text>
                        </View>
                        <View style={{ width: "50%", alignItems: "flex-end" }}>
                            <Text style={{ fontSize: 16, color: paletteOrange }}>{formatNumber(hook.data.calculation?.freight)}đ</Text>
                        </View>
                    </View> */}
                </View>
            </ScrollView>

            <View style={{
                backgroundColor: "white",
                padding: 10
            }}>
                <View style={{ flexDirection: "row", marginBottom: 20 }}>
                    <View style={{
                        width: "60%",
                        rowGap: 10
                    }}>
                        <Text style={{ fontSize: 16, color: palettePink }}>Tổng tiền</Text>
                        <Text style={{ fontSize: 20, color: paletteGreenBold }}>{formatNumber(hook.data.calculation?.total)}đ</Text>
                    </View>
                    <View style={{
                        width: "40%",
                        alignItems: "flex-end"
                    }}>
                        <Button
                            title="Đặt hàng"
                            onPress={hook.event.onSubmit}
                            buttonStyle={{ marginTop: 10, width: 140, backgroundColor: palettePink }} />
                    </View>
                </View>
            </View>
        </>
    )
}

export default CreateConfirmOrder