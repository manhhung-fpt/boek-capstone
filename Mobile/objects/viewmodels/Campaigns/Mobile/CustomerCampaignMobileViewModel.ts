import { HierarchicalCustomerCampaignMobileViewModel } from "./HierarchicalCustomerCampaignMobileViewModel";
import { UnhierarchicalCustomerCampaignMobileViewModel } from "./UnhierarchicalCustomerCampaignMobileViewModel";

export interface CustomerCampaignMobileViewModel {
    hierarchicalCustomerCampaigns: HierarchicalCustomerCampaignMobileViewModel[];
    unhierarchicalCustomerCampaigns: UnhierarchicalCustomerCampaignMobileViewModel[];
}