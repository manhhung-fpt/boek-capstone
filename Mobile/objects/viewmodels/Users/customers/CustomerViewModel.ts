import { BasicLevelViewModel } from "../../Levels/BasicLevelViewModel";
import { UserViewModel } from "../UserViewModel";

export interface CustomerViewModel {

    id: string;
    levelId: number;
    dob?: Date;
    gender?: boolean;
    point?: number;
    user: UserViewModel;
    level: BasicLevelViewModel;
}