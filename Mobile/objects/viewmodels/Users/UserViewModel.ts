import { AddressViewModel } from "../ Addresses/AddressViewModel";

export interface UserViewModel {
    id: string;
    code: string;
    name: string;
    email: string;
    address: string;
    addressViewModel: AddressViewModel;
    phone: string;
    roleName: string;
    role?: number;
    status: boolean;
    statusName: string;
    imageUrl: string;
}