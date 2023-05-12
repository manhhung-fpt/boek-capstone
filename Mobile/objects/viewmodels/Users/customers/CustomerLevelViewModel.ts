import { BasicLevelViewModel } from "../../Levels/BasicLevelViewModel";

export interface CustomerLevelViewModel {
    id?: string;
    levelId?: number;
    dob?: Date;
    gender: boolean;
    point: number;
    level: BasicLevelViewModel;
}