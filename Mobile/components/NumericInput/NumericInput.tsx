import React from 'react'
import { TextInput, TouchableOpacity, View } from 'react-native'
import { Text } from '@react-native-material/core'
interface NumericInputProps {
    minValue: number;
    maxValue: number;
    defaultValue?: number;
    value?: number;
    onChange: (value: number) => void;
}
function NumericInput(props: NumericInputProps) {
    return (
        <View style={{
            borderWidth: 1,
            width: "100%",
            height: "100%",
            flexDirection: "row"
        }}>
            <TouchableOpacity style={{
                borderRightWidth: 1,
                width: "20%",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <Text style={{ fontSize: 20 }}>-</Text>
            </TouchableOpacity>
            <View style={{
                width: "60%"
            }}>
                <TextInput
                    style={{
                        textAlign: "center"
                    }}
                    keyboardType='numeric'
                    defaultValue={props.defaultValue?.toString()}
                    value={props.value?.toString()}
                    onChangeText={(value) => {
                        const valueInt = parseInt(value);

                    }} />
            </View>
            <TouchableOpacity style={{
                borderLeftWidth: 1,
                width: "20%",
                alignItems: "center",
                justifyContent: "center"

            }}>
                <Text style={{ fontSize: 20 }}>+</Text>
            </TouchableOpacity>
        </View>
    )
}

export default NumericInput