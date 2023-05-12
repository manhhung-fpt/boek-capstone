import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useState } from "react";
import { MobileBookProductViewModel } from "../../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel";

export default function useBookItemsPage(props: StackScreenProps<ParamListBase>) {
    const [bookProduct, setBookProduct] = useState<MobileBookProductViewModel>();
    useEffect(() => {
        const params = props.route.params as { data: MobileBookProductViewModel };
        setBookProduct(params.data);        
        props.navigation.setOptions({
            title: params.data.title
        });
    }, []);

    return {
        data: {
            bookProduct
        }
    }
}