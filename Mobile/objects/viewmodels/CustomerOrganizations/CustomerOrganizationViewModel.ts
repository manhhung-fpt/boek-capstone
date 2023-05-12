import { BasicOrganizationViewModel } from "../Organizations/BasicOrganizationViewModel";
import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface CustomerOrganizationViewModel {
    id?: number;
    customerId?: string;
    organizationId?: number;
    customer?: CustomerUserViewModel;
    organization?: BasicOrganizationViewModel;
}
