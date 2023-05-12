import { BasicCampaignViewModel } from "../Campaigns/BasicCampaignViewModel";
import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface OrganizationViewModel {
    id?: number;
    name: string;
    address: string;
    phone: string;
    imageUrl: string;
    customers: CustomerUserViewModel[];
    campaigns: BasicCampaignViewModel[];
}