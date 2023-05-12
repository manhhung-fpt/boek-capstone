import { PropsWithChildren } from "react";
import { ActivityIndicator, StyleProp, View, ViewStyle } from "react-native";

interface StateLoaderProps extends PropsWithChildren {
    loading: boolean;
    style?: StyleProp<ViewStyle>;
}
function StateLoader(props: StateLoaderProps) {
    return (
        <View style={props.style}>
            {
                props.loading ? <View style={{ height: "100%", justifyContent: "center" }}><ActivityIndicator /></View> : props.children
            }
        </View>
    )
}

export default StateLoader