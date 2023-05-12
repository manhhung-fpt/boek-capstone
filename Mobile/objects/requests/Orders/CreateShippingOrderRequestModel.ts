import { AddressRequestModel } from "../Address/AddressRequestModel";
import { CreateOrderDetailsRequestModel } from "../OrderDetails/CreateOrderDetailsRequestModel";

export interface CreateShippingOrderRequestModel {
    campaignId?: number;
    addressRequest: AddressRequestModel;
    freight?: number;
    payment: number;
    orderDetails: CreateOrderDetailsRequestModel[];
}
