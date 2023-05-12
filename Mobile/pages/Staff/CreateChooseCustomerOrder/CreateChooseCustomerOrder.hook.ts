import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useState } from "react";
import appxios from "../../../components/AxiosInterceptor";
import { getMaxPage } from "../../../libs/functions/paging";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { StaffCampaignMobilesViewModel } from "../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel";
import { MultiUserViewModel } from "../../../objects/viewmodels/Users/MultiUserViewModel";
import endPont from "../../../utils/endPoints";

export default function useCreateChooseCustomerOrderPage(props: StackScreenProps<ParamListBase>) {
    const params = props.route.params as { campaign: StaffCampaignMobilesViewModel };
    const [loading, setLoading] = useState(false);
    const [currentPage, setCurrentPage] = useState(1);
    const [maxPage, setMaxPage] = useState(0);
    const [seletedCustomer, setSeletedCustomer] = useState<MultiUserViewModel>();
    const [customers, setCustomers] = useState<MultiUserViewModel[]>([]);
    const [name, setName] = useState("");

    const onPageNavigation = (page: number) => {
        getCustomer(page);
    };
    const getCustomer = (page: number) => {
        const query = new URLSearchParams();
        query.append("Role", "4");
        query.append("Status", "true");
        query.append("Page", page.toString());
        query.append("Size", "30");
        if (name != "") {
            query.append("Name", name);
        }
        setLoading(true);
        appxios.get<BaseResponsePagingModel<MultiUserViewModel>>(`${endPont.users.index}?${query.toString()}`).then(response => {
            setCustomers(response.data.data);
            setCurrentPage(getMaxPage(response.data.metadata.size, response.data.metadata.total));
            setLoading(false);
        }).finally(() => {
            setLoading(false);
        });
    }
    const onSearchSubmit = () => {
        getCustomer(1);
    }
    useEffect(() => {
        console.log(params);
        getCustomer(1);
    }, []);
    return {
        input: {
            seletedCustomer: {
                value: seletedCustomer,
                set: setSeletedCustomer
            },
            name: {
                value: name,
                set: setName
            }
        },
        event: {
            onSearchSubmit
        },
        data: {
            customers,
            params
        },
        paging: {
            currentPage,
            maxPage,
            onPageNavigation
        },
        ui: {
            loading
        }
    };
}