import { BasicOrganizationViewModel } from "../Organizations/BasicOrganizationViewModel";
import { CustomerOrganizationsViewModel } from "../Organizations/Mobile/CustomerOrganizationsViewModel";
import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface OwnedCustomerOrganizationViewModel {
    id?: number;
    customerId?: string;
    customer?: CustomerUserViewModel;
    organizations?: CustomerOrganizationsViewModel[];
}