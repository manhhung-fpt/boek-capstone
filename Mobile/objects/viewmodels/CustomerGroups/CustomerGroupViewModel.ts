import { BasicGroupViewModel } from "../Groups/BasicGroupViewModel";
import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface CustomerGroupViewModel {
    id?: number;
    customerId?: string;
    groupId?: number;
    customer?: CustomerUserViewModel;
    group?: BasicGroupViewModel;
}