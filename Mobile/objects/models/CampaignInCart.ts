import { BasicCampaignViewModel } from "../viewmodels/Campaigns/BasicCampaignViewModel";
import { CampaignMobileViewModel } from "../viewmodels/Campaigns/Mobile/CampaignMobileViewModel";
import { IssuerInCart } from "./IssuerInCart";

export interface CampaignInCart {
    campaign: BasicCampaignViewModel;
    issuersInCart: IssuerInCart[];
}