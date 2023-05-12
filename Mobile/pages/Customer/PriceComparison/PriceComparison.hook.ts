import { useEffect, useState } from "react";
import { MobileBookProductViewModel } from "../../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel";
import { PriceComparisonProps } from "./PriceComparison";

export default function usePriceComparisonPage(props: PriceComparisonProps) {
    const [bookProduct, setBookProduct] = useState<MobileBookProductViewModel>();
    useEffect(() => {
        console.log(props.route.params);
        const params = props.route.params as { data: MobileBookProductViewModel };
        setBookProduct(params.data);
    }, []);
    return {
        data: {
            bookProduct
        }
    };
}