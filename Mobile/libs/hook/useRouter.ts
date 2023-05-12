import {  NavigationContainerRefWithCurrent, ParamListBase, useNavigation } from "@react-navigation/native";
import { StackNavigationProp } from "@react-navigation/stack";
import { createRef } from "react";
export default function useRouter() {
    return useNavigation<StackNavigationProp<ParamListBase>>();
}
export const route = createRef<NavigationContainerRefWithCurrent<ParamListBase>>();
