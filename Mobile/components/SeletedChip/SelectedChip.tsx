import React from 'react'
import { TouchableOpacity, Text, TouchableOpacityProps, ColorValue } from 'react-native'
import useSelectedChipComponent from './SelectedChip.hook'
export interface SelectedChipProps extends TouchableOpacityProps {
    selected?: boolean;
    label: string;
    selectedBackgroundColor?: ColorValue | undefined;
    unSelectedBackgroundColor?: ColorValue | undefined;
    selectedTextColor?: ColorValue | undefined;
    unSelectedTextColor?: ColorValue | undefined;
}
function SelectedChip(props: SelectedChipProps) {
    const { getBackgroundColor, getTextColor } = useSelectedChipComponent(props);
    return (
        <TouchableOpacity style={{
            backgroundColor: getBackgroundColor(),
            borderRadius: 16,
            minHeight: 20,
            minWidth: 50,
            marginLeft : 5,
            marginRight : 5,
            alignItems: "center",
            justifyContent: "center",
            padding: 6
        }} {...props}>
            <Text style={{ color: getTextColor() , fontSize: 13 }}>{props.label}</Text>
        </TouchableOpacity>
    )
}

export default SelectedChip