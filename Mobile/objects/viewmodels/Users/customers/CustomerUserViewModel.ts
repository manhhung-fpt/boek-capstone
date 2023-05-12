import { UserViewModel } from "../UserViewModel";

export interface CustomerUserViewModel {
    levelId?: number;
    dob?: Date;
    gender: boolean;
    point?: number;
    user: UserViewModel;
}