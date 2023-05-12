import { UserViewModel } from "../UserViewModel";

export interface IssuerViewModel {
    id?: string;
    description?: string;
    user: UserViewModel;
}