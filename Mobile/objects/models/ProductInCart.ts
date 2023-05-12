export interface ProductInCart {
    id: string;
    quantity: number;
    title: string,
    imageUrl: string;
    pdfChecked: boolean;
    audioChecked: boolean;
    withPdf: boolean;
    withAudio: boolean;
    salePrice: number;
    pdfExtraPrice: number;
    audioExtraPrice: number;
    coverPrice?: number;
    discount?: number;
    saleQuantity: number;
}