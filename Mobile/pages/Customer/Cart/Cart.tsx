import { ParamListBase } from '@react-navigation/native'
import { StackScreenProps } from '@react-navigation/stack'
import { Button, CheckBox } from '@rneui/base'
import React from 'react'
import { ScrollView, View, Image, TouchableOpacity, Pressable } from 'react-native'
import { Text } from "@react-native-material/core";
import NumericInput from 'react-native-numeric-input'
import Close from '../../../assets/SvgComponents/Close'
import ExpandToggleView from '../../../components/ExpandToggleView/ExpandToggleView'
import useAppContext from '../../../context/Context'
import formatNumber from '../../../libs/functions/formatNumber'
import { paletteGray, paletteGrayTint9, palettePink, paletteRed, primaryTint1, primaryTint10, primaryTint4, primaryTint6, primaryTint8, primaryTint9 } from '../../../utils/color'
import useCartPage from './Cart.hook'
import Shadow from '../../../components/Shadow/Shadow'
import truncateString from '../../../libs/functions/truncateString'
import useRouter from '../../../libs/hook/useRouter'
import BouncyCheckbox from 'react-native-bouncy-checkbox'
import CartExpand from '../../../components/CartExpand/CartExpand'
import LayoutModal from '../../../components/LayoutModal/LayoutModal'
import QRCode from 'react-native-qrcode-svg'

function Cart(props: StackScreenProps<ParamListBase>) {
    const hook = useCartPage();
    const { push } = useRouter();
    const { cart } = useAppContext();
    return (
        <>
            <View style={{
                display: cart.length == 0 ? "flex" : "none",
                height: "90%",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <Text style={{ fontSize: 18 }}>Chưa có sản phẩm nào trong giỏ hàng</Text>
            </View>
            <ScrollView style={{
                backgroundColor: "white",
                height: "90%",
                display: cart.length == 0 ? "none" : "flex"
            }}>
                <View style={{
                    alignItems: "center",
                    paddingTop: 20
                }}>
                    {
                        cart.map((item, index) =>
                            <View style={{
                                width: "90%",
                                marginBottom: 40
                            }}>
                                <CartExpand
                                    onSelectedChange={() => hook.input.seletedCampaignId.set(item.campaign.id as number)}
                                    selected={hook.input.seletedCampaignId.value == item.campaign.id}
                                    campaignInCart={item} />
                            </View>
                        )
                    }
                </View>

            </ScrollView>
            <View style={{
                borderTopColor: paletteGray,
                borderTopWidth: 1,
                height: "10%",
                backgroundColor: "white",
                flexDirection: "row"
            }}>
                <View style={{
                    //borderWidth: 1,
                    width: "60%",
                    padding: 10
                }}>
                    <Text style={{ fontSize: 17, marginBottom: 10 }}>Tổng cộng</Text>
                    <Text style={{ fontSize: 16, color: palettePink }}>{hook.input.seletedCampaignId.value ? `${formatNumber(hook.getTotalPrice())}đ` : "Vui lòng chọn hội sách"}</Text>


                </View>
                <View style={{
                    //borderWidth : 1,
                    width: "40%",
                    alignItems: "center",
                    justifyContent: "center",
                }}>
                    {
                        hook.input.seletedCampaignId.value != 0 &&
                        <Button
                            onPress={() => push("OrderType", { seletedCampaignId: hook.input.seletedCampaignId.value })}
                            buttonStyle={{
                                display: cart.length == 0 ? "none" : "flex",
                                backgroundColor: primaryTint1,
                                width: 130
                            }} >Mua hàng</Button>
                    }
                </View>
            </View>
        </>
    )
}

export default Cart