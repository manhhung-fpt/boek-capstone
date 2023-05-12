import { CustomerLevelViewModel } from "./customers/CustomerLevelViewModel";
import { BasicIssuerViewModel } from "./issuers/BasicIssuerViewModel";

export interface MultiUserViewModel {
    id: string;
    code: string;
    name: string;
    email: string;
    address: string;
    phone: string;
    roleName: string;
    role?: number;
    status?: boolean;
    statusName: string;
    imageUrl: string;
    customer: CustomerLevelViewModel;
    issuer: BasicIssuerViewModel;
}