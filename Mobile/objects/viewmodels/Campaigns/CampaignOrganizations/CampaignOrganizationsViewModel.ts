import { BasicOrganizationViewModel } from "../../Organizations/BasicOrganizationViewModel";

export interface CampaignOrganizationsViewModel {
    id?: number;
    organizationId?: number;
    campaignId?: number;
    organization: BasicOrganizationViewModel;
}