import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface LevelViewModel {
    id?: number;
    name: string;
    conditionalPoint?: number;
    status?: boolean;
    statusName: string;
    customers?: CustomerUserViewModel[];
}