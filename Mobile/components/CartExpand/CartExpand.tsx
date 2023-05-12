import React, { useState } from 'react'
import { View, Image, TouchableOpacity } from 'react-native'
import { Text } from '@react-native-material/core'
import { BasicCampaignViewModel } from '../../objects/viewmodels/Campaigns/BasicCampaignViewModel'
import Shadow from '../Shadow/Shadow'
import BouncyCheckbox from 'react-native-bouncy-checkbox'
import { paletteGray, paletteGrayLight, paletteGrayShade1, paletteGrayShade4, paletteGrayShade6, paletteGrayTint3, paletteGrayTint5, paletteGrayTint6, palettePink, paletteRed, primaryColor, primaryTint1, primaryTint4 } from '../../utils/color'
import { IssuerViewModel } from '../../objects/viewmodels/Users/issuers/IssuerViewModel'
import expandMore from "../../assets/icons/expand-more-black.png";
import expandLess from "../../assets/icons/expand-less-black.png";
import { ProductInCart } from '../../objects/models/ProductInCart'
import truncateString from '../../libs/functions/truncateString'
import Close from '../../assets/SvgComponents/Close'
import useAppContext from '../../context/Context'
import useCartExpandComponent from './CartExpand.hook'
import formatNumber from '../../libs/functions/formatNumber'
import { CampaignInCart } from '../../objects/models/CampaignInCart'
import CampaignStatus from '../../objects/enums/CampaignStatus'
import { Button } from '@rneui/base'
import NumericInput from 'react-native-numeric-input'
interface CartExpandProps {
    campaignInCart: CampaignInCart;
    selected: boolean;
    onSelectedChange: (selected: boolean) => void;
}
function CartExpand(props: CartExpandProps) {
    const { removeFromCart } = useAppContext();
    const [issuersExpand, setIssuersExpand] = useState<boolean[]>(Array(props.campaignInCart.issuersInCart.length).fill(false));
    const hook = useCartExpandComponent();
    return (
        <Shadow style={{
            elevation: 2,
            //borderWidth: 1,
            borderRadius: 12,
            backgroundColor: props.campaignInCart.campaign.status == CampaignStatus.start ? "white" : paletteGrayTint5,
            overflow: "hidden"
        }}>
            <View style={{
                padding: 10,
                marginBottom: 10,
                borderBottomWidth: 1,
                borderBottomColor: paletteGrayLight,
                flexDirection: "row"
            }}>
                <BouncyCheckbox
                    disabled={props.campaignInCart.campaign.status != CampaignStatus.start}
                    isChecked={props.selected}
                    onPress={props.onSelectedChange}
                    disableBuiltInState
                    fillColor={primaryTint1}
                    text={truncateString(props.campaignInCart.campaign.name, 4)} textStyle={{ textDecorationLine: "none" }} />
            </View>
            {
                props.campaignInCart.issuersInCart.map((issuer, issuerIndex) =>
                    <View style={{
                        padding: 10,
                        //marginBottom : 20,
                        borderBottomWidth: issuerIndex + 1 == props.campaignInCart.issuersInCart.length ? 0 : 0.5,
                        borderBottomColor: primaryTint4,
                    }}>
                        <TouchableOpacity
                            onPress={() => {
                                setIssuersExpand(
                                    [
                                        ...issuersExpand.slice(0, issuerIndex),
                                        !issuersExpand[issuerIndex],
                                        ...issuersExpand.slice(issuerIndex + 1)
                                    ]
                                )
                            }}
                            style={{
                                flexDirection: "row",
                                //borderWidth: 1
                            }}>
                            <View style={{
                                width: "90%"
                            }}>
                                <Text style={{ color: paletteGrayShade4 }}>{issuer.issuer.user.name}</Text>
                            </View>
                            <View style={{
                                //borderWidth: 1,
                                width: "10%",
                                alignItems: "flex-end"
                            }}>
                                <View >
                                    <Image source={issuersExpand[issuerIndex] ? expandLess : expandMore} style={{ width: 30, height: 30 }} resizeMode="contain" />
                                </View>
                            </View>
                        </TouchableOpacity>
                        {
                            issuer.productsInCart.map(product =>
                                <View style={{
                                    display: issuersExpand[issuerIndex] ? "flex" : "none",
                                    alignItems: "center",

                                }}>
                                    <View style={{
                                        height: 220,
                                        borderColor: paletteGray,
                                        //borderWidth: 1,
                                        padding: 10
                                    }}>
                                        <View style={{
                                            height: "70%",
                                            flexDirection: "row",
                                            marginTop: "5%"
                                        }}>
                                            <View style={{
                                                width: "30%",
                                                height: "100%",
                                                borderWidth: 1,
                                                borderColor: primaryTint4
                                            }}>
                                                <Image source={{ uri: product.imageUrl }} style={{ height: "100%" }} resizeMode="cover" />
                                            </View>
                                            <View style={{
                                                //borderWidth: 1,
                                                width: "60%",
                                                height: "100%",
                                                padding: 10,
                                                justifyContent: "center"
                                            }}>
                                                <Text style={{
                                                    fontSize: 20
                                                }}>{truncateString(product.title, 6)}</Text>
                                                <Text style={{ paddingTop: 2, color: palettePink, fontSize: 18, fontWeight: "700" }}>{formatNumber(product.salePrice)}đ</Text>
                                                {
                                                    product.discount != undefined && product.discount > 0 &&
                                                    <Text style={{ textDecorationLine: "line-through", fontSize: 16, color: paletteGray }}>{formatNumber(product.coverPrice)}đ</Text>
                                                }

                                                <View style={{
                                                    flexDirection: "row",
                                                    height: 40,
                                                    paddingTop: 2
                                                }}>

                                                    <NumericInput
                                                        minValue={1}
                                                        maxValue={product.saleQuantity}
                                                        totalWidth={90}
                                                        value={product.quantity}
                                                        onChange={(value) => hook.event.onQuantityChange(props.campaignInCart.campaign.id as number, issuer.issuer.id as string, product.id, value)}
                                                    />
                                                </View>
                                                <View style={{
                                                    //borderWidth: 1,
                                                    marginTop: 25,
                                                    //flexDirection: "row",
                                                    //justifyContent: "center"

                                                }}>
                                                    {
                                                        product.withPdf &&
                                                        <View style={{ flexDirection: "row" }}>
                                                            <View style={{ width: "50%" }}>
                                                                <BouncyCheckbox
                                                                    disableBuiltInState
                                                                    onPress={checked => hook.event.onPdfChecked(props.campaignInCart.campaign.id as number, issuer.issuer.id as string, product.id)}
                                                                    isChecked={product.pdfChecked}
                                                                    size={15}
                                                                    textStyle={{ textDecorationLine: "none" }}
                                                                    fillColor={primaryTint1}
                                                                    text="PDF"
                                                                />
                                                            </View>
                                                            <Text style={{ color: palettePink }}>+ {formatNumber(product.pdfExtraPrice)}đ</Text>
                                                        </View>

                                                    }
                                                    <View style={{ height: 5 }}></View>
                                                    {
                                                        product.withAudio &&
                                                        <View style={{ flexDirection: "row" }}>
                                                            <View style={{ width: "50%" }}>
                                                                <BouncyCheckbox
                                                                    disableBuiltInState
                                                                    onPress={checked => hook.event.onAudioChecked(props.campaignInCart.campaign.id as number, issuer.issuer.id as string, product.id)}
                                                                    isChecked={product.audioChecked}
                                                                    textStyle={{ textDecorationLine: "none" }}
                                                                    size={15}
                                                                    fillColor={primaryTint1}
                                                                    text="Audio" />
                                                            </View>
                                                            <Text style={{ color: palettePink }}>+ {formatNumber(product.audioExtraPrice)}đ</Text>
                                                        </View>
                                                    }
                                                </View>
                                            </View>
                                            <View style={{
                                                //borderWidth: 1,
                                                width: "10%",
                                                alignItems: 'center',
                                                justifyContent: "center"
                                            }}>
                                                <TouchableOpacity
                                                    onPress={() => removeFromCart(props.campaignInCart.campaign.id as number, issuer.issuer.id as string, product.id)}>
                                                    <Close width={25} height={25} color={paletteRed} />
                                                </TouchableOpacity>
                                            </View>
                                        </View>

                                    </View>
                                </View>
                            )
                        }
                    </View>
                )
            }
        </Shadow>
    )
}

export default CartExpand