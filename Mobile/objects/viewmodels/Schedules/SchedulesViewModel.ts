import { AddressViewModel } from "../ Addresses/AddressViewModel";

export interface SchedulesViewModel {
    id?: number;
    campaignOrganizationId?: number;
    address?: string;
    addressViewModel?: AddressViewModel;
    startDate?: Date;
    endDate?: Date;
    status?: number;
    statusName?: string;
}