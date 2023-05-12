import { useEffect, useRef } from "react";
import { Animated, Easing } from "react-native";
import useBoolean from "../../libs/hook/useBoolean";
import { OrganizationViewProps } from "./OrganizationView";

export default function useOrganizationViewComponent(props: OrganizationViewProps) {
    const [expand, toogleExpand] = useBoolean();
    const animationHeight = useRef(new Animated.Value(0)).current;
    useEffect(() => {
        if (expand) {
            Animated.timing(animationHeight, {
                duration: 100,
                toValue: 360,
                easing: Easing.linear,
                useNativeDriver: false
            }).start();
        }
        else {
            Animated.timing(animationHeight, {
                duration: 100,
                toValue: 160,
                easing: Easing.linear,
                useNativeDriver: false
            }).start();
        }
    }, [expand]);
    return { toogleExpand, animationHeight, expand };
}