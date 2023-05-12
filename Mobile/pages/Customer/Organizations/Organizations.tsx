import { ScrollView, Image, View } from 'react-native'
import trackChange from "../../../assets/icons/track-changes-white.png";
import corporateFlare from "../../../assets/icons/corporate-fare-white.png";
import { useUnTrackedOrganizationsPage } from './Organizations.hook';
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs';
import { primaryColor, primaryTint1 } from '../../../utils/color';
import OrganizationView from '../../../components/OrganizationView/OrganizationView';
import Paging from '../../../components/Paging/Paging';
import PageLoader from '../../../components/PageLoader/PageLoader';
import StickyHeaderSearchBar from '../../../components/StickyHeaderSearchBar/StickyHeaderSearchBar';

const Tab = createBottomTabNavigator();

function Organizations() {
    const hook = useUnTrackedOrganizationsPage();
    return (
        <>
            <PageLoader loading={hook.loading} />
            <ScrollView
                style={{
                    backgroundColor: "white"
                }}
                ref={hook.ref.scrollViewRef}
                stickyHeaderHiddenOnScroll
                stickyHeaderIndices={[0]}>
                <StickyHeaderSearchBar
                    onSubmit={() => hook.event.getOrganization(1)}
                    onChangeText={hook.input.search.set}
                    value={hook.input.search.value} />
                <View style={{
                    padding: 20
                }}>
                    {
                        hook.data.organizations.map((item, index) =>
                            <OrganizationView
                                tracked={hook.input.trackedOrganizationIds.find(id => id == item.id) != undefined}
                                organization={item} />
                        )
                    }
                </View>

                <Paging currentPage={hook.paging.currentPage} maxPage={hook.paging.maxPage} onPageNavigation={hook.paging.onPageNavigation} />
            </ScrollView>
        </>
    )
}

export default Organizations