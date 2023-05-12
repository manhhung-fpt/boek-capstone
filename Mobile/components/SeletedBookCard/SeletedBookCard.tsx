import React from 'react'
import { TouchableOpacity, View, Image, GestureResponderEvent } from 'react-native'
import { Portal, Text } from '@react-native-material/core'
import { paletteGray, paletteGrayLight, paletteGrayShade5, palettePink, palettePinkTint2, palettePinkTint4, palettePinkTint6, paletteRed, primaryTint2, primaryTint7 } from '../../utils/color'
import { MobileBookProductViewModel } from '../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel';
import formatNumber from '../../libs/functions/formatNumber';
import { BookProductStatus } from '../../objects/enums/BookProductStatus';
import Shadow from '../Shadow/Shadow';
import truncateString from '../../libs/functions/truncateString';
interface SeletedBookCardProps {
    book: MobileBookProductViewModel;
    seleted: boolean;
    onPress?: (event: GestureResponderEvent) => void;
}
function SeletedBookCard(props: SeletedBookCardProps) {
    const getStatusBackgrundColor = (statusId: number) => {
        if (
            statusId == BookProductStatus.NotSale ||
            statusId == BookProductStatus.NotSaleDueCancelledCampaign ||
            statusId == BookProductStatus.NotSaleDueEndDate ||
            statusId == BookProductStatus.NotSaleDuePostponedCampaign ||
            statusId == BookProductStatus.Rejected) {
            return paletteRed;
        }
        if (
            statusId == BookProductStatus.OutOfStock ||
            statusId == BookProductStatus.Pending ||
            statusId == BookProductStatus.Unreleased) {
            return paletteGrayShade5;
        }
        if (statusId == BookProductStatus.Sale) {
            return primaryTint2;
        }
        return "blue";
    }
    return (
        <View
            style={{
                width: 195,
                height: 240,
                alignItems: "center",
                justifyContent: "center",
            }}>
            <Shadow style={{
                display: props.book.status == BookProductStatus.Sale ? "none" : "flex",
                backgroundColor: getStatusBackgrundColor(props.book.status as number),
                position: "absolute",
                borderRadius: 8,
                top: 20,
                right: 0,
                zIndex: 1,
                padding: 10
            }}>
                <Text style={{ color: "white", fontSize: 15 }}>{BookProductStatus.toDisplayString(props.book.status as number)}</Text>
            </Shadow>
            <TouchableOpacity
                disabled={props.book.status != BookProductStatus.Sale}
                onPress={props.onPress}
                style={{
                    borderColor: props.seleted ? palettePink : primaryTint7,
                    borderWidth: props.seleted ? 2 : 1,
                    borderRadius: 8,
                    height: "97%",
                    width: "95%"
                }}>
                <View style={{
                    display: props.book.status != BookProductStatus.Sale ? "flex" : "none",
                    position: "absolute",
                    backgroundColor: "rgba(0,0,0,0.3)",
                    height: "100%",
                    width: "100%",
                    zIndex: 5,
                    overflow: "hidden",
                    borderRadius: 8
                }}>

                </View>
                <View
                    style={{
                        height: "60%",
                        alignItems: "center",
                        justifyContent: "center"
                    }}>
                    <View
                        style={{
                            height: "95%",
                            width: "95%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                        <Image
                            source={{ uri: props.book.imageUrl }}
                            resizeMode="cover"
                            style={{ height: "90%", width: "90%" }} />
                    </View>
                </View>
                <View
                    style={{
                        width: "100%",
                        height: "40%",
                        alignItems: "center",
                        justifyContent: "center"
                    }}>
                    <View style={{
                        width: "95%",
                        height: "90%"
                    }}>
                        <View style={{ height: "30%", paddingLeft: 2 }}>
                            <Text style={{
                                fontSize: 16,
                                fontWeight: "600"
                            }}>{truncateString(props.book.title, 4)}</Text>
                        </View>
                        <View style={{ height: "70%", width: "100%", flexDirection: "row" }}>
                            <View style={{ width: "60%", height: "100%", justifyContent: "center", paddingLeft: 2 }}>
                                <Text style={{
                                    color: palettePink,
                                    fontSize: 18,
                                    fontWeight: "700"
                                }}>{formatNumber(props.book.salePrice)}đ</Text>
                                <Text style={{ fontSize: 16, color: paletteGray }}>SL bán: {formatNumber(props.book.saleQuantity)}</Text>
                            </View>
                            <View style={{ width: "40%", height: "100%", alignItems: "flex-start", justifyContent: "flex-start" }}>
                                {
                                    props.book.discount ?
                                        <View style={{
                                            width: "90%",
                                            backgroundColor: palettePink,
                                            alignItems: "center"
                                        }}>
                                            <Text style={{ color: "white", fontSize: 18, padding: 5 }}>-{props.book.discount}%</Text>
                                        </View>
                                        :
                                        null
                                }
                            </View>
                        </View>

                    </View>
                </View>
            </TouchableOpacity>
        </View>
    )
}

export default SeletedBookCard