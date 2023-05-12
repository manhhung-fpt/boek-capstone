import { OrderDetailsCalculationViewModel } from "../../OrderDetails/Calculation/OrderDetailsCalculationViewModel";

export interface OrderCalculationViewModel {
    subTotal?: number;
    freight?: number;
    discountTotal?: number;
    total?: number;
    freightName?: string;
    orderDetails?: OrderDetailsCalculationViewModel[];
}