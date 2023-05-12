import { CustomerGroupsViewModel } from "../Groups/CustomerGroupsViewModel";
import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface OwnedCustomerGroupViewModel {
    id?: number;
    customerId?: string;
    customer?: CustomerUserViewModel;
    groups?: CustomerGroupsViewModel[];
  }