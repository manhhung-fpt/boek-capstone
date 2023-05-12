export interface OrderDetailsCalculationViewModel {
    bookProductId?: string;
    quantity: number;
    price?: number;
    discount?: number;
    withPdf?: boolean;
    withAudio?: boolean;
    total?: number;
    subTotal?: number;
}