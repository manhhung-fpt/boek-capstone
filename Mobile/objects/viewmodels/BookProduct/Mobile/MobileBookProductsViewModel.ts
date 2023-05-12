import { BookProductItemViewModel } from "../../BookProductItems/BookProductItemViewModel";
import { BookViewModel } from "../../Books/BookViewModel";
import { IssuerViewModel } from "../../Users/issuers/IssuerViewModel";

export interface MobileBookProductsViewModel {
    id: string;
    bookId?: number;
    genreId?: number;
    campaignId?: number;
    issuerId?: string;
    title: string;
    description: string;
    imageUrl: string;
    saleQuantity: number;
    discount?: number;
    salePrice: number;
    type?: number;
    typeName: string;
    format?: number;
    formatName: string;
    withPdf?: boolean;
    pdfExtraPrice?: number;
    displayPdfIndex?: number;
    withAudio?: boolean;
    displayAudioIndex?: number;
    audioExtraPrice?: number;
    status?: number;
    statusName: string;
    note: string;
    createdDate?: Date;
    updatedDate?: Date;
    withLevel: boolean;
    allowPurchasingByLevel: boolean;
    issuer?: IssuerViewModel;
    book?: BookViewModel;
    bookProductItems?: BookProductItemViewModel[];
}
