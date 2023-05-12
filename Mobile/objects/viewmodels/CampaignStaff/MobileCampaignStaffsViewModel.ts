import { UserViewModel } from "../Users/UserViewModel";

export interface MobileCampaignStaffsViewModel
{
    staffId : string;
    status : number;
    statusName : string;
    staff : UserViewModel;
}