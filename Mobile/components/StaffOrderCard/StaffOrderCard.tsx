import { Button } from '@rneui/base'
import moment from 'moment'
import React from 'react'
import { Pressable, View, Image, Text } from 'react-native'
import formatNumber from '../../libs/functions/formatNumber'
import truncateString from '../../libs/functions/truncateString'
import useRouter from '../../libs/hook/useRouter'
import { OrderStatus } from '../../objects/enums/OrderStatus'
import { OrderViewModel } from '../../objects/viewmodels/Orders/OrderViewModel'
import { paletteGray, paletteGrayShade2, paletteGrayShade5, paletteGreenShade1, palettePink, paletteRed, primaryTint2 } from '../../utils/color'
import { dateTimeFormat } from '../../utils/format'
import Shadow from '../Shadow/Shadow'
interface StaffOrderCardProps {
    order: OrderViewModel;
}
function StaffOrderCard(props: StaffOrderCardProps) {
    const { push } = useRouter();
    const getStatusBackgrundColor = (statusId: number) => {
        if (statusId == OrderStatus.Cancelled) {
            return paletteRed;
        }
        if (statusId == OrderStatus.Processing) {
            return paletteGrayShade5;
        }
        if (statusId == OrderStatus.PickUpAvailable) {
            return primaryTint2;
        }
        if (statusId == OrderStatus.Received) {
            return paletteGreenShade1;
        }
        return "blue";
    }

    return (
        <Shadow
            style={{
                elevation: 4,
                marginTop: 20,
                backgroundColor: "white",
                //borderWidth: 1,
                borderRadius: 12,
                padding: 15,
                width: "90%",
                //borderColor: paletteGray,
                //borderBottomWidth: 1
            }}>
            <Pressable
                onPress={() => push("OrderDetail", { order: props.order })}>
                <View>
                    <View style={{
                        height: 40,
                        width: 140,
                        position: "absolute",
                        top: 0,
                        right: 0
                    }}>
                        <View style={{
                            height: "90%",
                            backgroundColor: getStatusBackgrundColor(props.order.status as number),
                            alignItems: "center",
                            justifyContent: "center",
                            shadowColor: "#000",
                            borderRadius: 8,
                            shadowOffset: {
                                width: 0,
                                height: 12,
                            },
                            shadowOpacity: 0.58,
                            shadowRadius: 16.00,
                            elevation: 8
                        }}>
                            <Text style={{ fontSize: 15, color: "white" }}>{props.order.statusName}</Text>
                        </View>
                    </View>
                    <View style={{
                        //borderWidth: 1,
                        height: 130,
                        padding: 5,
                        flexDirection: "row"
                    }}>
                        <View style={{
                            //borderWidth: 1,
                            height: "100%",
                            width: "25%",
                            flexDirection: "row"
                        }}>
                            <Image
                                source={{ uri: props.order.orderDetails && props.order.orderDetails[0].bookProduct?.imageUrl }}
                                resizeMode="contain"
                                style={{ width: "100%" }} />
                        </View>
                        <View style={{
                            //borderWidth: 1,
                            paddingLeft: 10,
                            width: "45%",
                            justifyContent: "center"
                        }}>
                            <Text style={{ marginBottom: "2%", color: paletteGrayShade2 }}>{props.order.orderDetails && props.order.orderDetails[0].bookProduct?.issuer.user.name}</Text>
                            <Text style={{ marginBottom: "2%", fontSize: 16, fontWeight: "600" }}>{props.order.orderDetails && props.order.orderDetails[0].bookProduct?.title}</Text>
                            <Text>SL : x{props.order.orderDetails && props.order.orderDetails[0] && props.order.orderDetails[0].quantity}</Text>
                        </View>
                        <View style={{ width: "30%", justifyContent: "center", alignItems: "flex-end" }}>
                            <Text style={{ fontSize: 18, color: palettePink }}>{formatNumber(props.order.subTotal)}đ</Text>
                        </View>
                    </View>
                </View>

                <View style={{
                    //borderWidth: 1,
                    height: 90,
                    padding: 7,
                    flexDirection: "row"
                }}>
                    <View style={{ width: "100%", rowGap: 10 }}>
                        <Text style={{ fontSize: 14, color: paletteGray }}>Ngày đặt hàng: {moment(props.order.orderDate).format(dateTimeFormat)}</Text>
                        <Text style={{ color: paletteGrayShade2, fontSize: 15 }}>{truncateString(props.order.campaign?.name, 3)}</Text>
                    </View>
                    {/* {
                        props.order.status == OrderStatus.PickUpAvailable &&
                        <View style={{ width: "40%", alignItems: "flex-end", justifyContent: "center" }}>
                            <Button
                                //onPress={hook.event.onOrderSubmit}
                                buttonStyle={{ backgroundColor: palettePink }}>Thanh toán</Button>
                        </View>
                    } */}
                </View>
            </Pressable>
        </Shadow>
    )
}

export default StaffOrderCard