import { BasicGroupViewModel } from "../Groups/BasicGroupViewModel";

export interface CampaignGroupsViewModel {
    id: number;
    campaignId?: number;
    groupId?: number;
    group: BasicGroupViewModel;
}