import { CampaignViewModel } from "../CampaignViewModel";

export interface SubHierarchicalCustomerCampaignMobileViewModel {
    subTitle: string;
    organizationId: number;
    groupId: number;
    campaigns: CampaignViewModel[];
}