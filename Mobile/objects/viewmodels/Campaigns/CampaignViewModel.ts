import { CampaignCommissionsViewModel } from "../CampaignCommissions/CampaignCommissionsViewModel";
import { CampaignGroupsViewModel } from "../CampaignGroups/CampaignGroupsViewModel";
import { CampaignOrganizationsViewModel } from "./CampaignOrganizations/CampaignOrganizationsViewModel";
import { CampaignParticipationsViewModel } from "../Participants/CampaignParticipationsViewModel";

export interface CampaignViewModel {
    id?: number;
    code?: string;
    name: string;
    description: string;
    imageUrl: string;
    format?: number;
    address: string;
    startDate?: Date;
    endDate?: Date;
    isRecurring?: boolean;
    status: number;
    createdDate?: Date;
    updatedDate?: Date;
    statusName: string;
    formatName: string;
    campaignCommissions: CampaignCommissionsViewModel[];
    campaignOrganizations: CampaignOrganizationsViewModel[];
    campaignGroups: CampaignGroupsViewModel[];
    participants: CampaignParticipationsViewModel[];
}