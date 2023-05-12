import { MobileCampaignStaffsViewModel } from "../CampaignStaff/MobileCampaignStaffsViewModel";
import { SchedulesViewModel } from "../Schedules/SchedulesViewModel";
import { IssuerViewModel } from "../Users/issuers/IssuerViewModel";

export interface StaffCampaignMobilesViewModel {
    id: number;
    code?: string;
    name?: string;
    description?: string;
    imageUrl?: string;
    format?: number;
    address?: string;
    startDate?: Date;
    endDate: Date;
    isRecurring?: boolean;
    status?: number;
    createdDate?: Date;
    updatedDate?: Date;
    statusName?: string;
    formatName?: string;
    sort?: string;
    issuers?: IssuerViewModel[];
    campaignStaffs?: MobileCampaignStaffsViewModel[];
    schedules?: SchedulesViewModel[];
    selectedSchedule?: SchedulesViewModel;
}