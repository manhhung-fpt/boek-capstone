import React, { PropsWithChildren, RefObject, useRef } from 'react'
import { View, Text, ScrollView, Image } from 'react-native';
import { paletteGrayLight, paletteGrayTint6, paletteGreenBold } from '../../utils/color';
import { mockBooks } from '../../utils/mock';
import zaloPay from "../../assets/zalopay.webp";
import { DrawerLayout } from 'react-native-gesture-handler';
import { MobileBookProductViewModel } from '../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel';
interface OrderDetailDrawerLayoutProps extends PropsWithChildren {
    orders?: MobileBookProductViewModel[];
    drawerRef: RefObject<DrawerLayout>;
}
function OrderDetailDrawerLayout(props: OrderDetailDrawerLayoutProps) {
    const r = useRef<DrawerLayout>(null);
    const book = mockBooks[3];
    return (
        <DrawerLayout
            ref={props.drawerRef}
            drawerWidth={330}
            drawerPosition={"left"}
            drawerBackgroundColor={"white"}
            renderNavigationView={() =>
                <ScrollView style={{
                    padding: 10
                }}>
                    <Text style={{ fontSize: 20, fontWeight: "700", marginBottom: 20 }}>Chi tiết đơn hàng</Text>
                    <Text style={{ fontSize: 16, fontWeight: "600", marginBottom: 10 }}>Danh sách sản phẩm</Text>

                    <View style={{
                        //borderWidth: 1,
                        height: 80,
                        flexDirection: "row",
                        borderBottomColor: paletteGrayLight,
                        borderBottomWidth: 1
                    }}>
                        <View style={{
                            //borderWidth: 1,
                            height: "100%",
                            width: "20%",
                            justifyContent: "center",
                        }}>
                            <Image source={{ uri: "https://th.bing.com/th/id/OIP.iWXhpy2Qtp630wf8zbD1IgHaHa?w=187&h=187&c=7&r=0&o=5&dpr=1.3&pid=1.7" }}
                                resizeMode="contain"
                                style={{
                                    height: "80%"
                                }} />
                        </View>

                        <View style={{
                            //borderWidth: 1,
                            width: "55%",
                            height: "100%",
                            justifyContent: "center"
                        }}>
                            <Text style={{ fontWeight: "600", paddingLeft: 2 }}>Thao túng thị trường rau sạch</Text>
                        </View>
                        <View style={{
                            //borderWidth: 1,
                            width: "25%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <Text style={{ fontWeight: "600" }}>69.000 đ</Text>
                        </View>
                    </View>

                    <View style={{
                        //borderWidth: 1,
                        height: 80,
                        flexDirection: "row",
                        borderBottomColor: paletteGrayLight,
                        borderBottomWidth: 1
                    }}>
                        <View style={{
                            //borderWidth: 1,
                            height: "100%",
                            width: "20%",
                            justifyContent: "center",
                        }}>
                            <Image source={{ uri: "https://th.bing.com/th/id/OIP.iWXhpy2Qtp630wf8zbD1IgHaHa?w=187&h=187&c=7&r=0&o=5&dpr=1.3&pid=1.7" }}
                                resizeMode="contain"
                                style={{
                                    height: "80%"
                                }} />
                        </View>

                        <View style={{
                            //borderWidth: 1,
                            width: "55%",
                            height: "100%",
                            justifyContent: "center"
                        }}>
                            <Text style={{ fontWeight: "600", paddingLeft: 2 }}>Thao túng thị trường rau sạch</Text>
                        </View>
                        <View style={{
                            //borderWidth: 1,
                            width: "25%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <Text style={{ fontWeight: "600" }}>69.000 đ</Text>
                        </View>
                    </View>

                    <View style={{
                        //borderWidth: 1,
                        height: 80,
                        flexDirection: "row",
                        borderBottomColor: paletteGrayLight,
                        borderBottomWidth: 1
                    }}>
                        <View style={{
                            //borderWidth: 1,
                            height: "100%",
                            width: "20%",
                            justifyContent: "center",
                        }}>
                            <Image source={{ uri: "https://th.bing.com/th/id/OIP.iWXhpy2Qtp630wf8zbD1IgHaHa?w=187&h=187&c=7&r=0&o=5&dpr=1.3&pid=1.7" }}
                                resizeMode="contain"
                                style={{
                                    height: "80%"
                                }} />
                        </View>

                        <View style={{
                            //borderWidth: 1,
                            width: "55%",
                            height: "100%",
                            justifyContent: "center"
                        }}>
                            <Text style={{ fontWeight: "600", paddingLeft: 2 }}>Thao túng thị trường rau sạch</Text>
                        </View>
                        <View style={{
                            //borderWidth: 1,
                            width: "25%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <Text style={{ fontWeight: "600" }}>69.000 đ</Text>
                        </View>
                    </View>


                    <View style={{ padding: 8, flexDirection: "row", borderColor: paletteGrayLight, borderTopWidth: 1, width: "100%" }}>
                        <View style={{ width: "50%" }}>
                            <Text >Tổng cộng:</Text>
                        </View>
                        <View style={{ width: "50%", alignItems: "flex-end" }}>
                            <Text >69.000 đ</Text>
                        </View>
                    </View>

                    <View style={{ padding: 8, flexDirection: "row", borderColor: paletteGrayLight, borderTopWidth: 1, width: "100%" }}>
                        <View style={{ width: "50%" }}>
                            <Text >Phí vận chuyển:</Text>
                        </View>
                        <View style={{ width: "50%", alignItems: "flex-end" }}>
                            <Text >69.000 đ</Text>
                        </View>
                    </View>

                    <View style={{ padding: 8, flexDirection: "row", borderColor: paletteGrayLight, borderTopWidth: 1, width: "100%" }}>
                        <View style={{ width: "50%" }}>
                            <Text >Giảm giá:</Text>
                        </View>
                        <View style={{ width: "50%", alignItems: "flex-end" }}>
                            <Text >69.000 đ</Text>
                        </View>
                    </View>

                    <View style={{ padding: 8, flexDirection: "row", borderColor: paletteGrayLight, borderTopWidth: 1, width: "100%" }}>
                        <View style={{ width: "50%" }}>
                            <Text style={{ fontSize: 16, fontWeight: "600" }}>Thành tiền:</Text>
                        </View>
                        <View style={{ width: "50%", alignItems: "flex-end" }}>
                            <Text style={{ color: paletteGreenBold, fontSize: 16, fontWeight: "600" }} >69.000 đ</Text>
                        </View>
                    </View>

                    <View style={{
                        padding: 8,
                        borderColor: paletteGrayLight,
                        borderTopWidth: 1,
                        width: "100%",
                        marginBottom : 20
                    }}>
                        <Text style={{ fontSize: 16, fontWeight: "600", marginBottom : 10 }}>Phương thức thanh toán</Text>
                        <View style={{
                            borderWidth: 1,
                            borderColor: paletteGrayTint6,
                            borderRadius: 4,
                            flexDirection: "row",
                            height: 50,
                            padding: 10
                        }}>
                            <View style={{
                                //borderWidth: 1,
                                width: "50%",
                                flexDirection: "row",
                                alignItems: "center"
                            }}>
                                <Image source={zaloPay} resizeMode="contain"
                                    style={{ height: "100%", width: "20%" }} />
                                <Text style={{ paddingLeft: 10 }}>ZaloPay</Text>
                            </View>
                            <View style={{
                                //borderWidth: 1,
                                width: "50%",
                                alignItems : "flex-end",
                                justifyContent : "center"
                            }}>
                                <Text>******696969</Text>
                            </View>
                        </View>
                    </View>

                </ScrollView>
            }>
            {props.children}
        </DrawerLayout>
    )
}

export default OrderDetailDrawerLayout