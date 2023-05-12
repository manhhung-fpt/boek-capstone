import React from 'react'
import { View, Text, Image, TouchableOpacity } from 'react-native'
import cartIcon from "../../assets/icons/shopping-basket-white.png";
import ShoppingCart from '../../assets/SvgComponents/ShoppingCart';
import useAppContext from '../../context/Context';
import useRouter from '../../libs/hook/useRouter';
import { primaryTint3, primaryTint9 } from '../../utils/color';
function CartHeader() {
    const { navigate } = useRouter();
    const { totalProductQuantity } = useAppContext();
    return (
        <TouchableOpacity
            onPress={() => navigate("Cart")}>
            {
                totalProductQuantity != 0 &&
                <View style={{
                    backgroundColor: primaryTint3,
                    position: "absolute",
                    width: 18,
                    height: 18,
                    zIndex: 1,
                    borderRadius: 999,
                    top: 0,
                    right: 0,
                    justifyContent: "center",
                    alignItems: "center"
                }}>
                    <Text style={{
                        fontSize: 11,
                        color: "white"
                    }}>{totalProductQuantity}</Text>
                </View>
            }
            <ShoppingCart width={35} height={35} fill={"white"} />
        </TouchableOpacity>
    )
}

export default CartHeader