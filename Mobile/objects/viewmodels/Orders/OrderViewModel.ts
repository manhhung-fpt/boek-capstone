import { BasicCampaignViewModel } from "../Campaigns/BasicCampaignViewModel";
import { OrderDetailsViewModel } from "../OrderDetails/OrderDetailsViewModel";
import { CustomerUserViewModel } from "../Users/customers/CustomerUserViewModel";

export interface OrderViewModel {
    id?: string;
    code?: string;
    customerId?: string;
    campaignId?: number;
    campaignStaffId?: number;
    customerName?: string;
    customerPhone?: string;
    customerEmail?: string;
    address?: string;
    freight?: number;
    payment?: number;
    paymentName?: string;
    type?: number;
    typeName?: string;
    note?: string;
    orderDate?: Date;
    availableDate?: Date;
    shippingDate?: Date;
    shippedDate?: Date;
    receivedDate?: Date;
    cancelledDate?: Date;
    status?: number;
    statusName?: string;
    total?: number;
    subTotal?: number;
    discountTotal?: number;
    freightName?: string;
    campaign?: BasicCampaignViewModel;
    //campaignStaff?: OrderCampaignStaffViewModel;
    customer?: CustomerUserViewModel;
    orderDetails?: OrderDetailsViewModel[];
}

export interface OrderCreateModel {
    id?: string;
    customerId?: string;
    campaignId: number;
    payment: number;
    address: string;
    type?: number;
    note?: string;
}

export interface OrderCreateModelIncludeHeader {
    id?: string;
    customerId?: string;
    customerName?: string;
    customerPhone?: string;
    customerEmail?: string;
    campaignId: number;
    status?: number;
    payment?: string;
    address?: string;
    type?: number;
    note?: string;
}

export interface OrderUpdateModel {
    id?: string;
    note?: string;
}