import React from 'react'
import { View, Text, ImageSourcePropType, Image, Dimensions, TouchableOpacity, GestureResponderEvent, Pressable } from 'react-native'
import { paletteGrayLight } from '../../utils/color';
interface LabeledImageProps {
    source: ImageSourcePropType;
    label?: string;
    touchable?: boolean;
    onPress?: (event: GestureResponderEvent) => void;
}
function LabeledImage(props: LabeledImageProps) {
    return (
        <View
            style={{
                height: 90,
                alignItems: "center",
                justifyContent: "center"
            }}>
            <Pressable
                onPress={props.onPress}
                style={{
                    alignItems: "center",
                    justifyContent: "center"
                }}>
                <View
                    style={{
                        borderWidth: 1,
                        borderColor: paletteGrayLight,
                        width: 50,
                        height: 50,
                        borderRadius: 999,
                        overflow: "hidden",
                        marginBottom: 5
                    }}>
                    <Image source={props.source} style={{ width: 50, height: 50 }} resizeMode="cover" />
                </View>
                <View>
                    <Text style={{ fontSize: 15, fontWeight: "600" }}>{props.label}</Text>
                </View>
            </Pressable>
        </View>
    )
}

export default LabeledImage