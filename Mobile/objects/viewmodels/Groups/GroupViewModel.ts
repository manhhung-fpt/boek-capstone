import { BasicCampaignViewModel } from "../Campaigns/BasicCampaignViewModel";
import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface GroupViewModel {
    id: number;
    name: string;
    description: string;
    status: boolean;
    statusName: string;
    customers: CustomerUserViewModel[];
    campaigns: BasicCampaignViewModel[];
}