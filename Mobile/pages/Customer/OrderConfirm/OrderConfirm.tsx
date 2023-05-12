import React from 'react'
import { Pressable, ScrollView, View, Image, TouchableOpacity } from 'react-native'
import { Button, Text } from '@react-native-material/core';
import LayoutModal from '../../../components/LayoutModal/LayoutModal'
import { StackScreenProps } from '@react-navigation/stack';
import { ParamListBase } from '@react-navigation/native';
import useOrderConfirmPage from './OrderConfirm.hook';
import { paletteGrayShade4, paletteGreenBold, paletteOrange, palettePink, primaryTint1, primaryTint4 } from '../../../utils/color';
import navigateRightBlack from "../../../assets/icons/navigate-right-black.png";
import ExpandToggleView from '../../../components/ExpandToggleView/ExpandToggleView';
import formatNumber from '../../../libs/functions/formatNumber';
import { CheckBox, Input } from '@rneui/base';
import localShippingBlack from "../../../assets/icons/local-shipping-black.png"
import packageBlack from "../../../assets/icons/package-black.png"
import editIcon from "../../../assets/icons/edit.png";
import useAppContext from '../../../context/Context';
import Shadow from '../../../components/Shadow/Shadow';
import BouncyCheckbox from 'react-native-bouncy-checkbox';
import PageLoader from '../../../components/PageLoader/PageLoader';
import { OrderType } from '../../../objects/enums/OrderType';
import { OrderPayment } from '../../../objects/enums/OrderPayment';
import SelectDropdown from 'react-native-select-dropdown';
import { Province } from '../../../objects/enums/Province';
import { getMessage } from '../../../utils/Validators';
import { District } from '../../../objects/enums/Districts';
import { Ward } from '../../../objects/enums/Ward';


function OrderConfirm(props: StackScreenProps<ParamListBase>) {
    const { cart, user } = useAppContext();
    const hook = useOrderConfirmPage(props);
    return (
        <>
            <PageLoader loading={hook.ui.loading} opacity={hook.ui.opacity} />
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
                        {
                            hook.data.orderType == OrderType.PickUp &&
                            <>
                                <Text style={{ fontSize: 16 }}>
                                    • NPH sẽ phụ trách thông báo địa điểm của
                                    đơn nhận tại quầy cho bạn khi đơn hàng
                                    sẵn sàng.
                                </Text>
                                <Text style={{ fontSize: 16 }}>
                                    • Nếu bạn không nhận hàng tại quầy trong
                                    thời gian hội sách diễn ra, thì đơn hàng sẽ
                                    bị hủy.
                                </Text>
                                <Text style={{ fontSize: 16 }}>
                                    • Lưu ý: Boek không chịu trách nhiệm về đơn
                                    đổi trả của khách hàng. Xin liên hệ NPH về
                                    vấn đề này.
                                </Text>
                            </>
                        }
                        {
                            hook.data.orderType == OrderType.Shipping &&
                            <>
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
                            </>
                        }

                    </View>
                </Pressable>
            </LayoutModal>
            <ScrollView style={{
                backgroundColor: "white",
                //alignItems : "center"
            }}>
                <View style={{
                    alignItems: "center",
                    marginTop: 20,
                    marginBottom: 70,
                    marginLeft: 20,
                    marginRight: 20
                }}>
                    <Shadow style={{
                        elevation: 10,
                        backgroundColor: "white",
                        borderRadius: 8
                    }}>
                        <View style={{
                            borderBottomColor: primaryTint1,
                            borderBottomWidth: 1,
                            flexDirection: "row",
                            paddingBottom: 10
                        }}>
                            <View style={{
                                //borderWidth: 1,
                                width: "15%",
                                //height : "100%",
                                paddingTop: "5%",
                                alignItems: "center",
                                // justifyContent: "center"
                            }}>
                                <Image source={hook.data.orderType == OrderType.Shipping ? localShippingBlack : packageBlack} resizeMode="contain" style={{ height: 25, width: 25 }} />
                            </View>
                            <View style={{
                                //borderWidth: 1,
                                width: "85%",
                                rowGap: 7,
                                marginTop: 10,
                            }}>
                                <Text style={{ fontSize: 14 }}>Tên: {user?.name}</Text>
                                <Text style={{ fontSize: 14 }}>SĐT: {user?.phone}</Text>
                                {
                                    hook.data.orderType == OrderType.Shipping &&
                                    <>
                                        <Text style={{ marginTop: 10, marginBottom: 10, fontSize: 14, fontWeight: "600" }}>Nơi giao hàng:</Text>
                                        <View style={{ flexDirection: "row" }}>
                                            <View style={{
                                                width: "20%",
                                                justifyContent: "center",
                                            }}>
                                                <Text style={{ fontSize: 14 }}>Tỉnh:</Text>
                                            </View>
                                            <View style={{
                                                width: "65%"
                                            }}>
                                                <SelectDropdown
                                                    defaultValueByIndex={hook.data.provincesSelect.findIndex(p => p.code == hook.input.province.value?.code)}
                                                    ref={hook.ref.inputProvinceRef}
                                                    renderDropdownIcon={() => <></>}
                                                    buttonStyle={{
                                                        width: "100%",
                                                        //alignItems: "center",
                                                        //justifyContent: "center",
                                                        backgroundColor: "white"
                                                    }}
                                                    buttonTextStyle={{ fontSize: 14, textAlign: "left", color: "black" }}
                                                    defaultButtonText="Chọn địa điểm"
                                                    onChangeSearchInputText={() => { console.log("Hello") }}
                                                    data={hook.data.provincesSelect}
                                                    onSelect={(selectedItem, index) => hook.event.onProvinceSelected(selectedItem)}
                                                    buttonTextAfterSelection={(selectedItem, index) => {
                                                        return (selectedItem as Province).nameWithType
                                                    }}
                                                    rowTextForSelection={(item, index) => {
                                                        return (item as Province).nameWithType
                                                    }}
                                                />
                                            </View>
                                            <View style={{
                                                width: "15%",
                                                alignItems: "center",
                                                justifyContent: "center"
                                            }}>
                                                <TouchableOpacity onPress={() => hook.ref.inputProvinceRef.current?.openDropdown()}>
                                                    <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                                                </TouchableOpacity>
                                            </View>
                                        </View>

                                        <View style={{ flexDirection: "row" }}>
                                            <View style={{
                                                width: "20%",
                                                justifyContent: "center",
                                            }}>
                                                <Text style={{ fontSize: 14 }}>Quận:</Text>
                                            </View>
                                            <View style={{
                                                width: "65%"
                                            }}>
                                                <SelectDropdown
                                                    onFocus={hook.event.onDistrictSelectedFocus}
                                                    ref={hook.ref.inputDistrictRef}
                                                    defaultValue={hook.data.districtSelect.find(p => p.code == hook.input.district.value?.code)}
                                                    renderDropdownIcon={() => <></>}
                                                    buttonStyle={{
                                                        width: "100%",
                                                        //alignItems: "center",
                                                        //justifyContent: "center",
                                                        backgroundColor: "white"
                                                    }}
                                                    buttonTextStyle={{ fontSize: 14, textAlign: "left", color: "black" }}
                                                    defaultButtonText="Chọn địa điểm"
                                                    onChangeSearchInputText={() => { console.log("Hello") }}
                                                    data={hook.data.districtSelect}
                                                    onSelect={(selectedItem, index) => hook.event.onDistrictSelected(selectedItem)}
                                                    buttonTextAfterSelection={(selectedItem, index) => {
                                                        return (selectedItem as District).nameWithType
                                                    }}
                                                    rowTextForSelection={(item, index) => {
                                                        return (item as District).nameWithType
                                                    }}
                                                />
                                            </View>
                                            <View style={{
                                                width: "15%",
                                                alignItems: "center",
                                                justifyContent: "center"
                                            }}>
                                                <TouchableOpacity onPress={() => hook.ref.inputDistrictRef.current?.openDropdown()}>
                                                    <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                                                </TouchableOpacity>
                                            </View>
                                        </View>

                                        <View style={{ flexDirection: "row" }}>
                                            <View style={{
                                                width: "20%",
                                                justifyContent: "center",
                                            }}>
                                                <Text style={{ fontSize: 14 }}>Huyện:</Text>
                                            </View>
                                            <View style={{
                                                width: "65%"
                                            }}>
                                                <SelectDropdown
                                                    onFocus={hook.event.onWardSelectedFocus}
                                                    ref={hook.ref.inputWardRef}
                                                    defaultValue={hook.data.wardSelect.find(p => p.code == hook.input.ward.value?.code)}
                                                    renderDropdownIcon={() => <></>}
                                                    buttonStyle={{
                                                        width: "100%",
                                                        //alignItems: "center",
                                                        //justifyContent: "center",
                                                        backgroundColor: "white"
                                                    }}
                                                    buttonTextStyle={{ fontSize: 14, textAlign: "left", color: "black" }}
                                                    defaultButtonText="Chọn địa điểm"
                                                    onChangeSearchInputText={() => { console.log("Hello") }}
                                                    data={hook.data.wardSelect}
                                                    onSelect={(selectedItem, index) => hook.event.onWardSelected(selectedItem)}
                                                    buttonTextAfterSelection={(selectedItem, index) => {
                                                        return (selectedItem as Ward).nameWithType
                                                    }}
                                                    rowTextForSelection={(item, index) => {
                                                        return (item as Ward).nameWithType
                                                    }}
                                                />
                                            </View>
                                            <View style={{
                                                width: "15%",
                                                alignItems: "center",
                                                justifyContent: "center"
                                            }}>
                                                <TouchableOpacity onPress={() => hook.ref.inputWardRef.current?.openDropdown()}>
                                                    <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                                                </TouchableOpacity>
                                            </View>
                                        </View>

                                        <View style={{ flexDirection: "row" }}>
                                            <View style={{
                                                width: "20%",
                                                justifyContent: "center",
                                            }}>
                                                <Text style={{ fontSize: 14 }}>Địa chỉ:</Text>
                                            </View>
                                            <View style={{
                                                width: "65%"
                                            }}>
                                                <Input
                                                    style={{ fontSize: 14 }}
                                                    value={hook.input.address.value}
                                                    onChangeText={hook.input.address.set} />
                                            </View>
                                            <View style={{
                                                width: "15%",
                                                alignItems: "center",
                                                justifyContent: "center"
                                            }}>
                                                <TouchableOpacity onPress={() => hook.ref.inputAddressRef.current?.focus()}>
                                                    <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                                                </TouchableOpacity>
                                            </View>
                                        </View>
                                    </>
                                }
                                {
                                    hook.data.orderType == OrderType.PickUp &&
                                    <Text style={{ fontSize: 14 }}>Địa chỉ nhận hàng: {hook.data.seltedCampaign?.campaign.address}</Text>
                                }
                            </View>
                        </View>

                        <ExpandToggleView initExpanded label={hook.data.seltedCampaign?.campaign.name || ""}>
                            <View style={{ padding: 10, borderTopWidth: 1, borderTopColor: primaryTint4 }}>

                                {
                                    hook.data.seltedCampaign?.issuersInCart.map(issuer =>
                                        <>
                                            <Text style={{ fontSize: 16, marginBottom: 10, color: paletteGrayShade4 }}>{issuer.issuer.user.name}</Text>

                                            {
                                                issuer.productsInCart.map(product =>
                                                    <View style={{
                                                        flexDirection: "row",
                                                        marginBottom: 20
                                                    }}>
                                                        <View style={{
                                                            borderWidth: 1,
                                                            borderColor: primaryTint1,
                                                            width: "15%",
                                                            height: 85,
                                                            overflow: "hidden",
                                                            borderRadius: 8
                                                        }}>
                                                            <Image resizeMode='contain' style={{ height: "100%" }} source={{ uri: product.imageUrl }} />
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
                                                            <Text style={{ color: palettePink, fontSize: 18, fontWeight: "700" }}>{formatNumber(hook.getProductFinalPrice(product))}đ</Text>
                                                        </View>
                                                    </View>
                                                )
                                            }

                                        </>
                                    )
                                }
                            </View>
                        </ExpandToggleView>

                        <View style={{
                            borderBottomColor: primaryTint4,
                            borderBottomWidth: 1,
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

                            <BouncyCheckbox
                                isChecked={hook.input.paymentMethod.value == OrderPayment.ZaloPay}
                                onPress={() => hook.input.paymentMethod.set(OrderPayment.ZaloPay)}
                                disableBuiltInState
                                size={20}
                                fillColor={primaryTint1}
                                text="ZaloPay" textStyle={{ textDecorationLine: "none" }} />
                        </View>

                        <View style={{
                            rowGap: 7,
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
                            {
                                hook.data.orderType == OrderType.Shipping &&
                                <View style={{ flexDirection: "row", marginBottom: 10 }}>
                                    <View style={{ width: "50%" }}>
                                        <Text style={{ fontSize: 16 }}>Phí vận chuyển</Text>
                                    </View>
                                    <View style={{ width: "50%", alignItems: "flex-end" }}>
                                        <Text style={{ fontSize: 16 }}>{formatNumber(hook.data.calculation?.freight)}đ</Text>
                                    </View>
                                </View>
                            }

                            <View style={{ flexDirection: "row" }}>
                                <View style={{ width: "50%" }}>
                                    <Text style={{ fontSize: 16, color: palettePink }}>Tích điểm</Text>
                                </View>
                                <View style={{ width: "50%", alignItems: "flex-end" }}>
                                    <Text style={{ fontSize: 16, color: paletteOrange }}>{formatNumber(0)}</Text>
                                </View>
                            </View>
                        </View>
                    </Shadow>
                </View>
            </ScrollView>
            <View style={{
                backgroundColor: "white",
                padding: 10,
                height: "15%"
            }}>
                <View style={{ flexDirection: "row" }}>
                    <View style={{
                        width: "60%",
                        rowGap: 7,
                    }}>
                        <Text style={{ fontSize: 16, color: palettePink }}>Tổng tiền</Text>
                        <Text style={{ fontSize: 16 }}>(Giá đã bao gồm VAT)</Text>
                        <Text style={{ fontSize: 20, color: paletteGreenBold }}>{formatNumber(hook.data.calculation?.total)}đ</Text>
                    </View>
                    <View style={{
                        width: "40%",
                        alignItems: "flex-end",
                        justifyContent: "center"
                    }}>
                        <Button
                            onPress={hook.event.onSumbit}
                            title="Thanh toán"
                            //onPress={hook.event.onOrderSubmit}
                            style={{ alignItems: "center", justifyContent: "center", width: "100%", height: 50, backgroundColor: palettePink }} />
                    </View>
                </View>
            </View>
        </>
    )
}

export default OrderConfirm