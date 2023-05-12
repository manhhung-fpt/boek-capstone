import { AddressRequestModel } from "../../Address/AddressRequestModel";
import { CreateOrderDetailsRequestModel } from "../../OrderDetails/CreateOrderDetailsRequestModel";

export interface CreateZaloPayOrderRequestModel {
    campaignId?: number;
    customerId?: string;
    customerName?: string;
    customerPhone?: string;
    customerEmail?: string;
    type?: number;
    addressRequest?: AddressRequestModel;
    address?: string;
    freight?: number;
    payment?: number;
    description?: string;
    redirectUrl: string;
    orderDetails?: CreateOrderDetailsRequestModel[];
}
