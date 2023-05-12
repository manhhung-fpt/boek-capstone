import AsyncStorage from "@react-native-async-storage/async-storage";
import { useEffect, useState } from "react";
import appxios, { setAuthorizationBearer } from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import useAuth from "../../../libs/hook/useAuth";
import useDebounce from "../../../libs/hook/useDebounce";
import useRouter from "../../../libs/hook/useRouter";
import { Role } from "../../../objects/enums/Role";
import { CreateCustomerRequestModel } from "../../../objects/requests/Users/Customers/CreateCustomerRequestModel";
import { BaseResponseModel } from "../../../objects/responses/BaseResponseModel";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { OwnedCustomerGroupViewModel } from "../../../objects/viewmodels/CustomerGroups/OwnedCustomerGroupViewModel";
import { GroupViewModel } from "../../../objects/viewmodels/Groups/GroupViewModel";
import { LoginViewModel } from "../../../objects/viewmodels/Users/LoginViewModel";
import endPont from "../../../utils/endPoints";
import { SessionStorage } from "../../../utils/SessionStogare";
import StorageKey from "../../../utils/storageKey";
import { AskGenresProps } from "./AskGenres";


export default function useAskGenrePage(props: AskGenresProps) {
    const { setUser } = useAppContext();
    const { replace, goBack, canGoBack } = useRouter();

    const [search, setSearch] = useState("");
    const searchDebounce = useDebounce(search, 600);

    const [loading, setLoading] = useState(false);
    const [searchMessage, setSearchMessage] = useState("");

    const [groupsSelect, setGroupsSelect] = useState<GroupViewModel[]>([]);

    const [selectedGroups, setSelectedGroups] = useState<number[]>([]);
    const { setAuthorize } = useAuth();

    const getGroups = (name: string) => {
        setLoading(true);
        const query = new URLSearchParams();
        query.append("Name", name);
        appxios.get<BaseResponsePagingModel<GroupViewModel>>(`${endPont.public.groups.index}?${query.toString()}`).then(response => {
            if (response.data.data.length == 0) {
                setSearchMessage("Không tìm thấy thể loại bạn tìm kiếm");
            }
            else {
                setSearchMessage("");
            }
            setGroupsSelect(response.data.data);
        }).finally(() => {
            setLoading(false);
        });
    }

    const onAskGenresSubmit = (skiped: boolean) => {
        if (!props.skiped) {
            if (canGoBack()) {
                goBack();
                return;
            }
        }
        else {
            const request = JSON.parse(SessionStorage.getItem(StorageKey.createCustomerRequest) as string) as CreateCustomerRequestModel;
            request.groupIds = selectedGroups;
            appxios.post<BaseResponseModel<LoginViewModel>>(endPont.users.index, request).then(async response => {
                console.log(response.status);
                if (response.status == 200) {
                    await AsyncStorage.setItem(StorageKey.user, JSON.stringify(response.data.data));
                    setAuthorize([response.data.data.role.toString()]);
                    setUser(response.data.data);
                    setAuthorizationBearer(response.data.data.accessToken);
                    replace("Index");
                }
            }).finally(() => {
                setLoading(false);
            });
        }
    }

    const onGroupsSelected = (group: GroupViewModel) => {
        if (!props.skiped) {
            setLoading(true);
            if (selectedGroups.find(g => g == group.id)) {
                appxios.delete(`${endPont.public.groups.customer}/${group.id}`).then(response => {
                    if (response.status == 200) {
                        setSelectedGroups(selectedGroups.filter(g => g != group.id));
                    }
                }).finally(() => {
                    setLoading(false);
                });
            }
            else {
                appxios.post(`${endPont.public.groups.customer}`, { groupId: group.id }).then(response => {
                    //console.log(response.data);
                    if (response.status == 200) {
                        setSelectedGroups([...selectedGroups, group.id]);
                    }
                }).finally(() => {
                    setLoading(false);
                });

            }
        }
        else {
            if (selectedGroups.find(g => g == group.id)) {
                setSelectedGroups(selectedGroups.filter(g => g != group.id));
            }
            else {
                setSelectedGroups([...selectedGroups, group.id]);
            }
        }
    }
    useEffect(() => {
        getGroups("");
        if (!props.skiped) {
            setLoading(true);
            appxios.get<OwnedCustomerGroupViewModel>(endPont.public.groups.customer).then(response => {
                //console.log(response.data);
                if (response.data.groups) {
                    setSelectedGroups(response.data.groups.map(item => item.group.id as number));
                }
            }).finally(() => {
                setLoading(false);
            });
        }
    }, []);

    useEffect(() => {
        getGroups(search);
    }, [searchDebounce]);

    return {
        loading,
        searchMessage,
        data: {
            groupsSelect
        },
        input: {
            selectedGroups,
            search: {
                value: search,
                set: setSearch
            }
        },
        event: {
            onAskGenresSubmit,
            onGroupsSelected
        }
    };
}