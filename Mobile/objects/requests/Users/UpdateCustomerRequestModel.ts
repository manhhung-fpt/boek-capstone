import { maxDate, maxLength, required } from "../../../utils/Validators";
import { UpdateUserRequestModel } from "./UpdateUserRequestModel";

export interface UpdateCustomerRequestModel {
    dob?: Date;
    gender?: boolean;
    user?: UpdateUserRequestModel;
}