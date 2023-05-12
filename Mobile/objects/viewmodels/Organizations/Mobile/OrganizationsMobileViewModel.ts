import { SchedulesViewModel } from "../../Schedules/SchedulesViewModel";

export interface OrganizationsMobileViewModel {
    id?: number;
    name: string;
    address?: string;
    phone?: string;
    imageUrl?: string;
    schedules?: SchedulesViewModel[];
}