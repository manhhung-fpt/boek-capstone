import { useEffect, useRef, useState } from "react";
import { ScrollView } from "react-native";
import appxios from "../../../components/AxiosInterceptor";
import { getMaxPage } from "../../../libs/functions/paging";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";
import { OwnedCustomerOrganizationViewModel } from "../../../objects/viewmodels/CustomerOrganizations/OwnedCustomerOrganizationViewModel";
import { OrganizationViewModel } from "../../../objects/viewmodels/Organizations/OrganizationViewModel";
import endPont from "../../../utils/endPoints";

export function useUnTrackedOrganizationsPage() {
    const scrollViewRef = useRef<ScrollView>(null);

    const [loading, setLoading] = useState(false);
    const [buttonsLoading, setButtonsLoading] = useState<boolean[]>([]);

    const [maxPage, setMaxPage] = useState(0);
    const [currentPage, setCurrentPage] = useState(1);
    const [organizations, setOrganizations] = useState<OrganizationViewModel[]>([]);

    const [trackedOrganizationIds, setTrackedOrganizationIds] = useState<number[]>([]);
    const [search, setSearch] = useState("");

    const getOrganization = (page: number) => {
        setLoading(true);
        const query = new URLSearchParams();
        query.append("Name", search);
        query.append("Size", "15");
        query.append("Page", page.toString());
        appxios.get<BaseResponsePagingModel<OrganizationViewModel>>(`${endPont.public.organizations.index}?${query.toString()}`).then(response => {
            setOrganizations(response.data.data);
            setCurrentPage(page);
            setMaxPage(getMaxPage(response.data.metadata.size, response.data.metadata.total));
            setButtonsLoading(new Array<boolean>(response.data.data.length));
            scrollViewRef.current?.scrollTo({
                y : 0
            })
        }).finally(() => {
            setLoading(false);
        });
    }
    const onPageNavigation = (page: number) => {
        getOrganization(page);
    }
    const onToggleTrackPress = (organization: OrganizationViewModel, index: number) => {
        setButtonsLoading([
            ...buttonsLoading.slice(0, index),
            true,
            ...buttonsLoading.slice(index + 1)
        ]);
        //console.log(organization.id);

        if (trackedOrganizationIds.find(o => o == organization.id)) {
            appxios.delete(`${endPont.public.organizations.customer}/${organization.id}`).then(response => {
                //console.log(response);
                //console.log(response);
                if (response.status == 200) {
                    setTrackedOrganizationIds(trackedOrganizationIds.filter(o => o != organization.id));
                }
            }).finally(() => {
                setButtonsLoading([
                    ...buttonsLoading.slice(0, index),
                    false,
                    ...buttonsLoading.slice(index + 1)
                ]);
            });
        }
        else {
            appxios.post(`${endPont.public.organizations.customer}`, { organizationId: organization.id }).then(response => {
                //console.log(response);
                if (response.status == 200) {
                    setTrackedOrganizationIds([...trackedOrganizationIds, organization.id as number]);
                }
            }).finally(() => {
                setButtonsLoading([
                    ...buttonsLoading.slice(0, index),
                    false,
                    ...buttonsLoading.slice(index + 1)
                ]);
            });
        }
    }

    useEffect(() => {
        getOrganization(1);
        appxios.get<OwnedCustomerOrganizationViewModel>(`${endPont.public.organizations.customer}`).then(response => {
            if (response.data.organizations) {
                setTrackedOrganizationIds(response.data.organizations.map(item => item.organization.id as number));
            }
        });
    }, []);
    return {
        buttonsLoading,
        loading,
        ref: {
            scrollViewRef
        },
        data: {
            organizations
        },
        input: {
            trackedOrganizationIds,
            search: {
                value: search,
                set: setSearch
            }
        },
        event: {
            onToggleTrackPress,
            getOrganization
        },
        paging: {
            maxPage,
            currentPage,
            onPageNavigation
        }
    };
}