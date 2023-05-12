import { useEffect, useState } from "react";
import AsyncStorage from '@react-native-async-storage/async-storage';
import appxios, { setAuthorizationBearer } from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import useAuth from "../../../libs/hook/useAuth";
import useRouter from "../../../libs/hook/useRouter";
import { CreateCustomerRequestModel } from "../../../objects/requests/Users/Customers/CreateCustomerRequestModel";
import { BaseResponseModel } from "../../../objects/responses/BaseResponseModel";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { CustomerOrganizationViewModel } from "../../../objects/viewmodels/CustomerOrganizations/CustomerOrganizationViewModel";
import { OwnedCustomerOrganizationViewModel } from "../../../objects/viewmodels/CustomerOrganizations/OwnedCustomerOrganizationViewModel";
import { OrganizationViewModel } from "../../../objects/viewmodels/Organizations/OrganizationViewModel";
import { LoginViewModel } from "../../../objects/viewmodels/Users/LoginViewModel";
import endPont from "../../../utils/endPoints";
import { SessionStorage } from "../../../utils/SessionStogare";
import StorageKey from "../../../utils/storageKey";

export default function useAskOrganizationsPage() {
    const { setUser } = useAppContext();
    const { setAuthorize } = useAuth();
    const [loading, setLoading] = useState(false);

    const [searchMessage, setSearchMessage] = useState("");//"Không tìm thấy thể loại bạn tìm kiếm"
    const { replace } = useRouter();
    const [organizationsSelect, setOrganizationsSelect] = useState<OrganizationViewModel[]>([]);

    const [seletedOrganization, setSeletedOrganization] = useState<number[]>([]);

    const getOrganizations = (name: string) => {
        setLoading(true);
        const query = new URLSearchParams();
        query.append("Name", name);
        appxios.get<BaseResponsePagingModel<OrganizationViewModel>>(`${endPont.public.organizations.index}`).then(response => {
            setOrganizationsSelect(response.data.data);
        }).finally(() => {
            setLoading(false);
        });
    }

    const onOrganizationsSeleted = (organization: OrganizationViewModel) => {
        if (seletedOrganization.find(g => g == organization.id)) {
            setSeletedOrganization(seletedOrganization.filter(g => g != organization.id));
        }
        else {
            setSeletedOrganization([...seletedOrganization, organization.id as number]);
        }
    }
    const onAskOrganizationsSubmit = (skiped: boolean) => {
        const request = JSON.parse(SessionStorage.getItem(StorageKey.createCustomerRequest) as string) as CreateCustomerRequestModel;
        request.organizationIds = seletedOrganization;
        setLoading(true);
        appxios.post<BaseResponseModel<LoginViewModel>>(endPont.users.index, request).then(async response => {
            console.log(response);
            if (response.status == 200) {
                setUser(response.data.data);
                await AsyncStorage.setItem(StorageKey.user, JSON.stringify(response.data.data));
                setAuthorizationBearer(response.data.data.accessToken);
                setAuthorize([response.data.data.role.toString()]);
            }
        }).finally(() => {
            setLoading(false);
            replace("Index");
        });
    }

    useEffect(() => {
        getOrganizations("");
    }, []);
    return {
        loading,
        searchMessage,
        event: {
            onAskOrganizationsSubmit,
            onOrganizationsSeleted
        },
        data: {
            organizationsSelect
        },
        input: {
            seletedOrganization
        }
    };
}