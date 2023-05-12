import { IssuerViewModel } from "../viewmodels/Users/issuers/IssuerViewModel";
import { ProductInCart } from "./ProductInCart";

export interface IssuerInCart {
    issuer : IssuerViewModel;
    productsInCart : ProductInCart[];
}