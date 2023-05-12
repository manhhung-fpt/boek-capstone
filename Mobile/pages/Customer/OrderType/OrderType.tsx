import { ParamListBase } from '@react-navigation/native'
import { StackScreenProps } from '@react-navigation/stack'
import React from 'react'
import { View, Image, TouchableOpacity } from 'react-native'
import { Button, Text } from '@react-native-material/core';
import useOrderTypePage from './OrderType.hook'
import Shadow from '../../../components/Shadow/Shadow';
import localShippingBlack from "../../../assets/icons/local-shipping-black.png";
import localShippingWhite from "../../../assets/icons/local-shipping-white.png";
import packageBlack from "../../../assets/icons/package-black.png";
import packageOutlineWhite from "../../../assets/icons/package-outline-white.png";
import { paletteGray, paletteGrayLight, primaryColor, primaryTint1 } from '../../../utils/color';
import useRouter from '../../../libs/hook/useRouter';
import { CampaignFormat } from '../../../objects/enums/CampaignFormat';
function OrderType(props: StackScreenProps<ParamListBase>) {
    const localShippingOrder = 1;
    const packageOrder = 2;
    const hook = useOrderTypePage(props);
    const { push } = useRouter();
    return (
        <View style={{
            width: "100%",
            height: "100%",
            backgroundColor: "white"
        }}>
            <View style={{
                //borderWidth: 1,
                height: "15%",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <Text variant='h5' style={{ textAlign: "center" }}>Bạn muốn đơn hàng là loại nào sau đây?</Text>
            </View>
            <View style={{
                //borderWidth: 1,
                height: "30%",
                flexDirection: "row",
                alignItems: "center",
                justifyContent: "center",
                columnGap: 60
            }}>
                <View>
                    <Shadow style={{
                        backgroundColor: hook.input.orderType.value == localShippingOrder ? primaryColor : "white",
                        width: 120,
                        height: 120,
                        borderRadius: 24,
                    }}>
                        <TouchableOpacity
                            onPress={() => hook.input.orderType.set(localShippingOrder)}
                            style={{
                                alignItems: "center",
                                justifyContent: "center",
                                width: "100%",
                                height: "100%"
                            }}>
                            <Image source={hook.input.orderType.value == localShippingOrder ? localShippingWhite : localShippingBlack} style={{
                                width: "50%",
                                height: "50%"
                            }} />
                        </TouchableOpacity>
                    </Shadow>
                    <View style={{
                        alignItems: "center",
                        marginTop: "10%"
                    }}>
                        <Text style={{ fontSize: 18 }}>Giao hàng</Text>
                    </View>
                </View>

                <View>
                    <Shadow style={{
                        backgroundColor:
                            hook.data.campaign?.campaign.format == CampaignFormat.online ? paletteGray :
                                hook.input.orderType.value == packageOrder ? primaryColor : "white",
                        width: 120,
                        height: 120,
                        borderRadius: 24,
                    }}>
                        <TouchableOpacity
                            disabled={hook.data.campaign?.campaign.format == CampaignFormat.online}
                            onPress={() => hook.input.orderType.set(packageOrder)}
                            style={{
                                alignItems: "center",
                                justifyContent: "center",
                                width: "100%",
                                height: "100%",
                            }}>
                            <Image source={hook.input.orderType.value == packageOrder ? packageOutlineWhite : packageBlack} style={{
                                width: "50%",
                                height: "50%"
                            }} />
                        </TouchableOpacity>

                    </Shadow>
                    <View style={{
                        alignItems: "center",
                        marginTop: "10%"
                    }}>
                        <Text style={{ fontSize: 18 }}>Tại quầy</Text>
                    </View>
                </View>

            </View>

            <View style={{
                //borderWidth: 1,
                height: "40%",
                padding: 30
            }}>
                <Text style={{
                    display: hook.input.orderType.value == localShippingOrder ? "flex" : "none"
                }}>
                    • NPH sẽ phụ trách giao hàng đến địa chỉ của
                    bạn. {"\n\n"}
                    • Phí vận chuyển của đơn phụ thuộc vào nội
                    thành hay ngoại thành đối với các nơi hội
                    sách đang tổ chức {"\n\n"}
                    {"\t"}o Nội thành: 15,000 đ {"\n"}
                    {"\t"}o Ngoại thành: 30,000 đ {"\n\n"}
                    • Lưu ý: Boek không chịu trách nhiệm về đơn
                    đổi trả của khách hàng. Xin liên hệ NPH về
                    vấn đề này.{"\n\n"}
                </Text>
                <Text
                    style={{
                        display: hook.input.orderType.value == packageOrder ? "flex" : "none"
                    }}>
                    • NPH sẽ phụ trách thông báo địa điểm của
                    đơn nhận tại quầy cho bạn khi đơn hàng
                    sẵn sàng. {"\n\n"}
                    • Nếu bạn không nhận hàng tại quầy trong
                    thời gian hội sách diễn ra, thì đơn hàng sẽ
                    bị hủy.{"\n\n"}
                    • Lưu ý: Boek không chịu trách nhiệm về đơn
                    đổi trả của khách hàng. Xin liên hệ NPH về
                    vấn đề này.
                </Text>
            </View>
            {
                hook.input.orderType.value != 0 &&
                <View style={{
                    height: "10%",
                    alignItems: "center",
                    justifyContent: "center"
                }}>
                    <Button
                        onPress={() => push("OrderConfirm", { orderType: hook.input.orderType.value, selectedCampaignId: hook.data.params.seletedCampaignId })}
                        title="Tiếp theo"
                        style={{ backgroundColor: primaryTint1, width: 160 }} />
                </View>
            }
        </View>
    )
}

export default OrderType