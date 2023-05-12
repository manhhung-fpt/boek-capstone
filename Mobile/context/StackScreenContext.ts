import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { createContext } from "react";

const StackScreenContext = createContext<StackScreenProps<ParamListBase>>({} as StackScreenProps<ParamListBase>);
export default StackScreenContext;