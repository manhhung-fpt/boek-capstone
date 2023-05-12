import { ColorValue, TouchableOpacityProps } from "react-native";
import { SelectedChipProps } from "./SelectedChip";
export default function useSelectedChipComponent(props: SelectedChipProps)
{
    const getBackgroundColor = () =>
    {
        if (props.selected) {
            if (props.selectedBackgroundColor) {
                return props.selectedBackgroundColor;
            }
            return "#515659" as ColorValue;
        }
        else
        {
            if (props.unSelectedBackgroundColor) {
                return props.unSelectedBackgroundColor;
            }
            return "#c7cfd6" as ColorValue;
        }
    }
    const getTextColor = () =>
    {
        if (props.selected) {
            if (props.selectedTextColor) {
                return props.selectedTextColor;
            }
            return "white" as ColorValue;
        }
        else
        {
            if (props.unSelectedTextColor) {
                return props.unSelectedTextColor;
            }
            return "black" as ColorValue;
        }
    }
    return {getBackgroundColor, getTextColor };
}