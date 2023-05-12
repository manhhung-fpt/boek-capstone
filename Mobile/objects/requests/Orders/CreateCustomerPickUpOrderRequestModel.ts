import { CreateOrderDetailsRequestModel } from "../OrderDetails/CreateOrderDetailsRequestModel";

export interface CreateCustomerPickUpOrderRequestModel {
    campaignId?: number;
    payment: number;
    orderDetails: CreateOrderDetailsRequestModel[];
}