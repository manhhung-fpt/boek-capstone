import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useState } from "react";
import useRouter from "../../../libs/hook/useRouter";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import { emailAddress, maxLength, numberString, required, validate, ValidationMessages } from "../../../utils/Validators";

export function useCreateChooseHaveAccountOrderPage(props: StackScreenProps<ParamListBase>) {
    const params = props.route.params as { campaign: StaffCampaignMobilesViewModel };
    const { push } = useRouter();
    const [haveAccount, setHaveAccount] = useState<boolean>();

    const [name, setName] = useState("");
    const [phone, setPhone] = useState("");
    const [email, setEmail] = useState("");
    const [validationMessages, setValidationMessages] = useState<ValidationMessages>();

    const onSubmit = () => {
        if (haveAccount) {
            push("CreateChooseCustomerOrder", { campaign: params.campaign });
        }
        else {
            const validation = {
                name: [
                    required(name, "Tên không được để trống"),
                    maxLength(name, 128, "Tên không được vượt quá 128 ký tự")
                ],
                phone: [
                    required(phone, "Số điện thoại không được để trống"),
                    maxLength(phone, 10, "Số điện thoại không được vượt quá 10 số")
                ],
                email: [
                    required(email, "Email không được để trống"),
                    maxLength(email, 128, "Email không được vượt quá 128 ký tự"),
                    emailAddress(email, "Email không đúng định dạng")
                ]
            };
            setValidationMessages(validation);
            if (validate(validation)) {
                push("CreateChooseProductsOrder", { campaign: params.campaign, customer: { name, phone, email } });
            }
        }

    }

    const skip = () => {
        push("CreateChooseProductsOrder", { campaign: params.campaign });
    }
    return {
        event: {
            skip,
            onSubmit
        },
        input: {
            haveAccount: {
                value: haveAccount,
                set: setHaveAccount
            },
            email: {
                value: email,
                set: setEmail,
            },
            phone: {
                value: phone,
                set: setPhone
            },
            name: {
                value: name,
                set: setName
            }
        },
        ui: {
            validationMessages
        }
    }
}