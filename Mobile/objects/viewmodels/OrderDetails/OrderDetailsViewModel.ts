import { OrderBookProductsViewModel } from "../BookProducts/OrderBookProductsViewModel";

export interface OrderDetailsViewModel {
    id?: number;
    orderId?: string;
    bookProductId?: string;
    quantity: number;
    price?: number;
    discount?: number;
    withPdf?: boolean;
    withAudio?: boolean;
    total?: number;
    subTotal?: number;
    bookProduct?: OrderBookProductsViewModel;
}
