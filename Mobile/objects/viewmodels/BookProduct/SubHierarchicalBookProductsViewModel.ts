import { GenreViewModel } from "../Genres/GenreViewModel";
import { UserViewModel } from "../Users/UserViewModel";
import { MobileBookProductViewModel } from "./Mobile/MobileBookProductViewModel";

export interface SubHierarchicalBookProductsViewModel {
    subTitle: string;
    genreId?: number;
    issuerId?: string;
    genre?: GenreViewModel;
    issuer?: UserViewModel;
    bookProducts?: MobileBookProductViewModel[];
}