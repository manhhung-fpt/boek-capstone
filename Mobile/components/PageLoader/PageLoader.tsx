import { ActivityIndicator } from "@react-native-material/core";
import { View } from "react-native";

export default function PageLoader({ loading, zIndex, opacity }: { loading: boolean, zIndex?: number, opacity?: number }) {
    return (
        <>
            <View style={{
                backgroundColor: "white",
                display: loading ? "flex" : "none",
                opacity: opacity || 0.6,
                zIndex: zIndex || 998,
                position: "absolute",
                width: "100%",
                height: "100%",
                alignItems: "center",
                justifyContent: "center"
            }} />
            <View style={{
                backgroundColor: "transparent",
                display: loading ? "flex" : "none",
                zIndex: 999,
                opacity: 0.6,
                position: "absolute",
                width: "100%",
                height: "100%",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <ActivityIndicator
                    size="large" />
            </View>
        </>
    );
}