import { MobileBookProductViewModel } from "./MobileBookProductViewModel";

export interface UnhierarchicalBookProductsViewModel {
    campaignId?: number;
    title: string;
    bookProducts: MobileBookProductViewModel[];
}