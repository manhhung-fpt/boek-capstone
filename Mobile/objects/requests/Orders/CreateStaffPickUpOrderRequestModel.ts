import { CreateOrderDetailsRequestModel } from "../OrderDetails/CreateOrderDetailsRequestModel";

export interface CreateStaffPickUpOrderRequestModel {
    customerId?: string;
    campaignId: number;
    customerName?: string;
    customerPhone?: string;
    customerEmail?: string;
    address?: string;
    payment: number;
    orderDetails: CreateOrderDetailsRequestModel[];
}