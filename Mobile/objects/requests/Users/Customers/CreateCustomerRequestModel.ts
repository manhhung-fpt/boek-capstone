import { AddressRequestModel } from "../../Address/AddressRequestModel";

export interface CreateCustomerRequestModel {
    idToken: string;
    name?: string;
    gender?: boolean;
    address?: AddressRequestModel;
    Dob?: Date;
    phone?: string;
    organizationIds?: number[];
    groupIds?: number[];
}