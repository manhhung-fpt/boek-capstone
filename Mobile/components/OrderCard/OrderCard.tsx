import { Button } from '@rneui/base';
import React from 'react'
import { View, Text, Image } from 'react-native'
import useRouter from '../../libs/hook/useRouter';
import { paletteGray, paletteGrayShade2, paletteGrayTint5, paletteGreen, paletteGreenBold, paletteGreenShade1, palettePink, primaryTint1, primaryTint4 } from '../../utils/color'
import { mockBooks } from '../../utils/mock';
import navigateRightWhite from "../../assets/icons/navigate-right-black.png";

function OrderCard() {
    const { navigate } = useRouter();
    const orderBook = mockBooks.slice(0, 2);
    return (
        <View key={Math.random()}
            style={{
                backgroundColor: "white",
                borderBottomWidth: 1,
                borderColor: paletteGray,
                padding: 10
            }}>
            <View style={{ height: 40, flexDirection: "row" }}>
                <View style={{ width: "60%", justifyContent: "center" }}>
                    <Text style={{ fontSize: 15 }}>{"Mã đơn hàng"}</Text>
                </View>
                <View style={{ width: "40%" }}>
                    <View style={{
                        height: "90%",
                        backgroundColor: paletteGreenShade1,
                        alignItems: "center",
                        justifyContent: "center",
                        shadowColor: "#000",
                        shadowOffset: {
                            width: 0,
                            height: 12,
                        },
                        shadowOpacity: 0.58,
                        shadowRadius: 16.00,
                        elevation: 8
                    }}>
                        <Text style={{ fontSize: 15, color: "white" }}>{"Giao thành công"}</Text>
                    </View>
                </View>
            </View>
            <Text style={{ fontSize: 14, color: paletteGray }}>{"Ngày đặt hàng"}</Text>

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
                        source={{ uri: "https://salt.tikicdn.com/cache/280x280/ts/product/8a/c3/a9/733444596bdb38042ee6c28634624ee5.jpg" }}
                        resizeMode="contain"
                        style={{ width: "100%" }} />
                </View>
                <View style={{
                    //borderWidth: 1,
                    width: "45%",
                    justifyContent: "center"
                }}>
                    <Text style={{ marginBottom: "2%", color: paletteGrayShade2 }}>BIZBOOK</Text>
                    <Text style={{ marginBottom: "2%", fontSize: 16, fontWeight: "600" }}>Thao túng thị trường rau sạch</Text>
                    <Text>SL : x2</Text>
                </View>
                <View style={{ justifyContent: "center", alignItems: "center" }}>
                    <Text style={{ fontSize: 18, color: palettePink }}>69.000đ</Text>
                </View>
            </View>

            <View style={{
                //borderWidth: 1,
                height: 40,
                padding: 7,
                flexDirection: "row"
            }}>
                <View style={{ width: "90%" }}>
                    <Text style={{ color: paletteGrayShade2, fontSize: 15 }}>Tên hội sách</Text>
                </View>
                <View style={{ width: "10%", alignItems: "center", justifyContent: "center" }}>
                    <Image source={navigateRightWhite} style={{ width: 25, height: 25 }} resizeMode="contain" />
                </View>
            </View>
        </View>
    )
}

export default OrderCard