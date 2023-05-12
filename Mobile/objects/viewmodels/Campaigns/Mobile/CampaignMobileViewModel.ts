import { HierarchicalBookProductsViewModel } from "../../BookProduct/Mobile/HierarchicalBookProductsViewModel";
import { MobileBookProductsViewModel } from "../../BookProduct/Mobile/MobileBookProductsViewModel";
import { UnhierarchicalBookProductsViewModel } from "../../BookProduct/Mobile/UnhierarchicalBookProductsViewModel";
import { BasicGroupViewModel } from "../../Groups/BasicGroupViewModel";
import { BasicLevelViewModel } from "../../Levels/BasicLevelViewModel";
import { OrganizationsMobileViewModel } from "../../Organizations/Mobile/OrganizationsMobileViewModel";
import { IssuerViewModel } from "../../Users/issuers/IssuerViewModel";

export interface CampaignMobileViewModel {
    id?: number;
    code?: string;
    name?: string;
    description?: string;
    imageUrl?: string;
    format?: number;
    address?: string;
    startDate?: Date;
    endDate?: Date;
    isRecurring?: boolean;
    status?: number;
    createdDate?: Date;
    updatedDate?: Date;
    statusName?: string;
    formatName?: string;
    sort?: string;
    organizations?: OrganizationsMobileViewModel[];
    issuers?: IssuerViewModel[];
    groups?: BasicGroupViewModel[];
    levels?: BasicLevelViewModel[];
    bookProducts?: MobileBookProductsViewModel[];
    hierarchicalBookProducts?: HierarchicalBookProductsViewModel[];
    unhierarchicalBookProducts?: UnhierarchicalBookProductsViewModel[];
}
