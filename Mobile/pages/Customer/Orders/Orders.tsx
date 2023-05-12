import React from 'react'
import { View, Image, ScrollView, FlatList, ActivityIndicator, TouchableOpacity, Pressable, Dimensions, RefreshControl } from 'react-native'
import { Text } from "@react-native-material/core";
import logo from "../../assets/logo.png";
import Paging from '../../../components/Paging/Paging';
import range from '../../../libs/functions/range';
import { paletteGray, paletteGrayShade2, paletteGreenShade1, palettePink, paletteRed, primaryColor, primaryTint4 } from '../../../utils/color';
import SelectedChip from '../../../components/SeletedChip/SelectedChip';
import { createMaterialTopTabNavigator } from '@react-navigation/material-top-tabs';
import navigateRightBlack from "../../../assets/icons/navigate-right-black.png";
import { Button } from '@rneui/base';
import useRouter from '../../../libs/hook/useRouter';
import { ParamListBase } from '@react-navigation/native';
import { StackScreenProps } from '@react-navigation/stack';
import { useCounterOrdersPage, useDeliveryOrdersPage } from './Orders.hook';
import Shadow from '../../../components/Shadow/Shadow';
import PageLoader from '../../../components/PageLoader/PageLoader';
import moment from 'moment';
import { dateFormat, dateTimeFormat } from '../../../utils/format';
import formatNumber from '../../../libs/functions/formatNumber';
import { OrderStatus } from '../../../objects/enums/OrderStatus';
import truncateString from '../../../libs/functions/truncateString';
import LayoutModal from '../../../components/LayoutModal/LayoutModal';
import QRCode from 'react-native-qrcode-svg';
import StickyHeaderSearchBar from '../../../components/StickyHeaderSearchBar/StickyHeaderSearchBar';

const Tab = createMaterialTopTabNavigator();

function Orders(props: StackScreenProps<ParamListBase>) {
    const params = props.route.params as { name: string };
    return (
        <>

            <Tab.Navigator
                initialRouteName={params && params.name || "DeliveryOrders"}
                screenOptions={{
                    tabBarLabelStyle: {
                        color: primaryColor,
                        fontWeight: "500"
                    },
                    tabBarIndicatorStyle: {
                        backgroundColor: primaryColor
                    },
                    swipeEnabled: false,
                    lazy: true,
                    lazyPlaceholder: () => <ActivityIndicator size="large" style={{ height: "100%" }} />
                }}>
                <Tab.Screen options={{ title: "Đơn giao" }} name="DeliveryOrders" component={DeliveryOrders} />
                <Tab.Screen options={{ title: "Đơn tại quầy" }} name="CounterOrders" component={CounterOrders} />
            </Tab.Navigator>
        </>
    );
}

function DeliveryOrders() {
    const hook = useDeliveryOrdersPage();
    const { push } = useRouter();
    return (
        <>
            <PageLoader loading={hook.ui.loading} />
            <ScrollView
                style={{
                    backgroundColor: "white"
                }}
                ref={hook.ref.ordersScrollViewRef}
                stickyHeaderIndices={[0]}
                stickyHeaderHiddenOnScroll
                refreshControl={
                    <RefreshControl refreshing={hook.ui.refreshing}
                        onRefresh={hook.event.onRefresh} />}>
                <View style={{ justifyContent: "center" }}>
                    <FlatList
                        style={{
                            height: 50,
                            backgroundColor: "white",
                            borderBottomWidth: 1,
                            borderBottomColor: paletteGray,
                            //justifyContent: "center",
                            paddingLeft: 10
                        }}
                        horizontal
                        data={[
                            {
                                label: "Tất cả",
                                input: 0
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.Processing),
                                input: OrderStatus.Processing
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.Shipping),
                                input: OrderStatus.Shipping
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.Shipped),
                                input: OrderStatus.Shipped
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.Cancelled),
                                input: OrderStatus.Cancelled
                            }
                        ]}
                        renderItem={item =>
                            <View style={{ height: "100%", justifyContent: "center", marginRight: 2 }}>
                                <SelectedChip
                                    onPress={() => hook.input.orderStatus.set(item.item.input)}
                                    selected={hook.input.orderStatus.value == item.item.input}
                                    label={item.item.label} />
                            </View>
                        } />
                    <StickyHeaderSearchBar onChangeText={hook.input.search.set} onSubmit={hook.event.onSearchSubmit} value={hook.input.search.value} />
                </View>
                <View>
                    <View style={{
                        alignItems: "center"
                    }}>
                        {
                            hook.data.orders.length == 0 &&
                            <View style={{
                                alignItems: "center",
                                justifyContent: "center",
                                width: "100%",
                                height: Dimensions.get("screen").height * 80 / 100,
                            }}>
                                <Text variant='h5'>Không có đơn hàng</Text>
                            </View>
                        }
                        {
                            hook.data.orders.map(item =>
                                item.orderDetails && item.orderDetails[0] &&
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
                                        onPress={() => push("OrderDetail", { order: item })}>
                                        {/* <View style={{ height: 40, flexDirection: "row" }}>
                                            <View style={{ width: "60%", justifyContent: "center" }}>
                                                <Text style={{ fontSize: 15 }}>{item.id}</Text>
                                            </View>
                                            <View style={{ width: "40%" }}>
                                                <View style={{
                                                    height: "90%",
                                                    backgroundColor: hook.ui.getStatusBackgrundColor(item.status as number),
                                                    alignItems: "center",
                                                    justifyContent: "center",
                                                    shadowColor: "#000",
                                                    borderRadius : 8,
                                                    shadowOffset: {
                                                        width: 0,
                                                        height: 12,
                                                    },
                                                    shadowOpacity: 0.58,
                                                    shadowRadius: 16.00,
                                                    elevation: 8
                                                }}>
                                                    <Text style={{ fontSize: 15, color: "white" }}>{item.statusName}</Text>
                                                </View>
                                            </View>
                                        </View> */}
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
                                                    backgroundColor: hook.ui.getStatusBackgrundColor(item.status as number),
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
                                                    <Text style={{ fontSize: 15, color: "white" }}>{item.statusName}</Text>
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
                                                        source={{ uri: item.orderDetails[0].bookProduct?.imageUrl }}
                                                        resizeMode="contain"
                                                        style={{ width: "100%" }} />
                                                </View>
                                                <View style={{
                                                    //borderWidth: 1,
                                                    width: "45%",
                                                    justifyContent: "center",
                                                    padding: 10
                                                }}>
                                                    <Text style={{ marginBottom: "2%", color: paletteGrayShade2 }}>{item.orderDetails[0].bookProduct?.issuer.user.name}</Text>
                                                    <Text style={{ marginBottom: "2%", fontSize: 16, fontWeight: "600" }}>{truncateString(item.orderDetails[0].bookProduct?.title, 6)}</Text>
                                                    <Text>SL : x1</Text>
                                                </View>
                                                <View style={{ width: "30%", justifyContent: "center", alignItems: "flex-end" }}>
                                                    <Text style={{ fontSize: 18, color: palettePink }}>{formatNumber(item.orderDetails[0].total)}đ</Text>
                                                </View>
                                            </View>

                                            <View style={{
                                                //borderWidth: 1,
                                                height: 60,
                                                padding: 7,
                                                flexDirection: "row"
                                            }}>
                                                <View style={{ width: "90%", rowGap: 10 }}>
                                                    <Text style={{ fontSize: 14, color: paletteGray }}>Ngày đặt hàng: {moment(item.orderDate).format(dateTimeFormat)}</Text>
                                                    <Text style={{ color: paletteGrayShade2, fontSize: 15 }}>{truncateString(item.campaign?.name, 3)}</Text>
                                                </View>
                                                <View style={{ width: "10%", alignItems: "flex-end", justifyContent: "center" }}>
                                                    <Image source={navigateRightBlack} style={{ width: 25, height: 25 }} resizeMode="contain" />
                                                </View>
                                            </View>
                                        </View>
                                    </Pressable>
                                </Shadow>
                            )
                        }
                    </View>
                </View>
                <View style={{ marginBottom: 20, marginTop: 20 }}>
                    <Paging currentPage={hook.paging.currentPage} maxPage={hook.paging.maxPage} onPageNavigation={hook.paging.onPageNavigation} />
                </View>
            </ScrollView>
        </>
    );
}

function CounterOrders() {
    const { push } = useRouter();
    const hook = useCounterOrdersPage();
    return (
        <>
            <PageLoader loading={hook.ui.loading} />
            <LayoutModal visible={hook.ui.qrModalVisible} onClose={() => hook.ui.setQrModalVisible(!hook.ui.qrModalVisible)}>
                <Pressable
                    onPress={() => hook.ui.setQrModalVisible(false)}
                    style={{
                        width: "100%",
                        height: "100%",
                        alignItems: "center",
                        justifyContent: "center",
                        backgroundColor: "rgba(0,0,0,0.6)"
                    }}>
                    <View style={{
                        backgroundColor: "white",
                        width: 320,
                        height: 320,
                        padding: 10,
                        alignItems: "center",
                        justifyContent: "center"
                    }}>
                        <QRCode
                            size={200}
                            backgroundColor="white"
                            color="black"
                            value={hook.data.qrString} />
                        <Text style={{ fontSize: 16, textAlign: "center", marginTop: 10 }}>Đưa mã thanh toán này cho nhân viên</Text>
                    </View>
                </Pressable>
            </LayoutModal>
            <ScrollView
                style={{
                    backgroundColor: "white"
                }}
                ref={hook.ref.ordersScrollViewRef}
                stickyHeaderIndices={[0]}
                stickyHeaderHiddenOnScroll
                refreshControl={
                    <RefreshControl refreshing={hook.ui.refreshing}
                        onRefresh={hook.event.onRefresh} />}
                >
                <View>
                    <FlatList
                        style={{
                            height: 50,
                            backgroundColor: "white",
                            borderBottomWidth: 1,
                            borderBottomColor: paletteGray,
                            //justifyContent: "center",
                            paddingLeft: 10
                        }}
                        horizontal
                        data={[
                            {
                                label: "Tất cả",
                                input: 0
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.Processing),
                                input: OrderStatus.Processing
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.PickUpAvailable),
                                input: OrderStatus.PickUpAvailable
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.Received),
                                input: OrderStatus.Received
                            },
                            {
                                label: OrderStatus.getLabel(OrderStatus.Cancelled),
                                input: OrderStatus.Cancelled
                            }]}
                        renderItem={item =>
                            <View style={{ height: "100%", justifyContent: "center", marginRight: 2 }}>
                                <SelectedChip
                                    onPress={() => hook.input.orderStatus.set(item.item.input)}
                                    selected={hook.input.orderStatus.value == item.item.input}
                                    label={item.item.label} />
                            </View>
                        } />
                    <StickyHeaderSearchBar onChangeText={hook.input.search.set} onSubmit={hook.event.onSearchSubmit} value={hook.input.search.value} />
                </View>
                <View style={{
                    alignItems: "center"
                }}>
                    {
                        hook.data.orders.length == 0 &&
                        <View style={{
                            alignItems: "center",
                            justifyContent: "center",
                            width: "100%",
                            height: Dimensions.get("screen").height * 80 / 100,
                        }}>
                            <Text variant='h5'>Không có đơn hàng</Text>
                        </View>
                    }
                    {
                        hook.data.orders.map(item =>
                            item.orderDetails && item.orderDetails[0] &&

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
                                    onPress={() => push("OrderDetail", { order: item })}>
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
                                                backgroundColor: hook.ui.getStatusBackgrundColor(item.status as number),
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
                                                <Text style={{ fontSize: 15, color: "white" }}>{item.statusName}</Text>
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
                                                    source={{ uri: item.orderDetails[0].bookProduct?.imageUrl }}
                                                    resizeMode="contain"
                                                    style={{ width: "100%" }} />
                                            </View>
                                            <View style={{
                                                //borderWidth: 1,
                                                padding: 10,
                                                width: "45%",
                                                justifyContent: "center"
                                            }}>
                                                <Text style={{ marginBottom: "2%", color: paletteGrayShade2 }}>{item.orderDetails[0].bookProduct?.issuer.user.name}</Text>
                                                <Text style={{ marginBottom: "2%", fontSize: 16, fontWeight: "600" }}>{item.orderDetails[0].bookProduct?.title}</Text>
                                                <Text>SL : x2</Text>
                                            </View>
                                            <View style={{ width: "30%", justifyContent: "center", alignItems: "flex-end" }}>
                                                <Text style={{ fontSize: 18, color: palettePink }}>{formatNumber(item.orderDetails[0].price)}đ</Text>
                                            </View>
                                        </View>
                                    </View>

                                    <View style={{
                                        //borderWidth: 1,
                                        height: 90,
                                        padding: 7,
                                        flexDirection: "row"
                                    }}>
                                        <View style={{ width: "60%", rowGap: 10 }}>
                                            <Text style={{ fontSize: 14, color: paletteGray }}>Ngày đặt hàng: {moment(item.orderDate).format(dateTimeFormat)}</Text>
                                            <Text style={{ color: paletteGrayShade2, fontSize: 15 }}>{truncateString(item.campaign?.name, 3)}</Text>
                                        </View>
                                        <View style={{
                                            display: item.status == OrderStatus.PickUpAvailable ? "flex" : "none",
                                            width: "40%",
                                            alignItems: "flex-end",
                                            justifyContent: "center"
                                        }}>
                                            <Button
                                                onPress={() => hook.event.onShowQrPress(item.id as string)}
                                                buttonStyle={{ backgroundColor: palettePink }}>Thanh toán</Button>
                                        </View>
                                    </View>
                                </Pressable>
                            </Shadow>
                        )
                    }
                </View>
                <View style={{ marginTop: 20, marginBottom: 20 }}>
                    <Paging currentPage={hook.paging.currentPage} maxPage={hook.paging.maxPage} onPageNavigation={hook.paging.onPageNavigation} />
                </View>
            </ScrollView>
        </>
    );
}

export default Orders