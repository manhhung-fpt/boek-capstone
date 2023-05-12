import React from 'react'
import { View, Text, Image } from 'react-native'
import corporateFlare from "../../assets/icons/corporate-fare-black.png";
import { primaryTint7 } from '../../utils/color';


function OrganizationCard() {
    return (
        <View style={{ borderWidth: 1, borderColor: primaryTint7, height: 100, borderRadius: 8 }}>
            <View style={{ flex: 1, flexDirection: "row" }}>
                <View style={{ height: "100%", width: "25%", alignItems: "center", justifyContent: "center" }}>
                    <Image style={{ height: "80%", width: "80%" }} source={corporateFlare} resizeMode="contain" />
                </View>
                <View style={{ height: "100%", width: "75%", paddingLeft: 10 }}>
                    <View style={{ height: "100%", justifyContent: "center" }} >
                        <Text style={{ fontSize: 18 }}>FPT</Text>
                        <Text>Quận 9 - 0101456789</Text>
                    </View>
                </View>
            </View>
        </View>
    )
}

export default OrganizationCard