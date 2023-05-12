import { Slider, SliderProps } from '@rneui/base';
import React from 'react'
import { View } from 'react-native'
interface VerticalSliderProps extends SliderProps {
    upSideDown?: boolean;
}
function VerticalSlider(props: VerticalSliderProps) {
    return (
        <View style={{
            width: "100%",
            height: "100%",
            alignItems: "center",
            justifyContent: "center"
        }}>
            <Slider
                {...props}
                style={Object.assign({}, props.style, { transform: [{ rotate: props.upSideDown ? '90deg' : "270deg" }] })}
            />
        </View>

    )
}

export default VerticalSlider