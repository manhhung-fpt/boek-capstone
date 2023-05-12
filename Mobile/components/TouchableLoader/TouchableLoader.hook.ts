import { useState } from "react";
import { GestureResponderEvent } from "react-native";
import { TouchableLoaderProps } from "./TouchableLoader";

export default function useTouchableLoaderComponent(props: TouchableLoaderProps) {
    const [loading, setLoading] = useState(false);
    const doWork = async (e: GestureResponderEvent) => {
        setLoading(true);
        await props.onLoadingPress(e);
        setLoading(false);
    }
    return { loading, doWork };
}