import { IssuerViewModel } from "../Users/issuers/IssuerViewModel";

export interface CampaignParticipationsViewModel {

    id?: number;
    campaignId?: number;
    issuerId?: string;
    createdDate: Date;
    updatedDate: Date;
    status?: number;
    statusName: string;
    issuer: IssuerViewModel;
}