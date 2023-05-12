import React from 'react'
import { View, Text, TouchableOpacity, GestureResponderEvent } from 'react-native'
import { primaryTint10, primaryTint4, primaryTint8 } from '../../utils/color'
interface SelectedLabelProps {
    label: string;
    onPress?: (event: GestureResponderEvent) => void;
    seleted: boolean;
}
function SelectedLabel(props: SelectedLabelProps) {
    return (
        <TouchableOpacity
            onPress={props.onPress}
            style={{
                padding: 15,
                flexDirection: "row",
                borderColor: primaryTint4,
                backgroundColor: props.seleted ? primaryTint8 : primaryTint10
            }}>
            <View style={{ width: "85%" }}>
                <Text
                    style={{
                        marginLeft: 25,
                        fontSize: 15
                    }}>
                    {props.label}
                </Text>
            </View>
        </TouchableOpacity>
    )
}

export default SelectedLabel