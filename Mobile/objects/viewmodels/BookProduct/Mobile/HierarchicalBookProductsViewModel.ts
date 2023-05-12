import { SubHierarchicalBookProductsViewModel } from "../SubHierarchicalBookProductsViewModel";

export interface HierarchicalBookProductsViewModel {
    campaignId?: number;
    title: string;
    subHierarchicalBookProducts?: SubHierarchicalBookProductsViewModel[];
}