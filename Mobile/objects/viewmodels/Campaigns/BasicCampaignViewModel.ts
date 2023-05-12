import { AddressViewModel } from "../ Addresses/AddressViewModel";

export interface BasicCampaignViewModel {
    id?: number;
    code?: string;
    name: string;
    description: string;
    imageUrl: string;
    format?: number;
    address?: string;
    addressViewModel?: AddressViewModel;
    startDate?: Date;
    endDate?: Date;
    isRecurring?: boolean;
    status?: number;
    createdDate?: Date;
    updatedDate?: Date;
    formatName: string;
    statusName: string;
}