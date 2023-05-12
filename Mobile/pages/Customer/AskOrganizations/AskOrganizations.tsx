import { Button, Input } from '@rneui/base'
import React from 'react'
import { ScrollView, View, Text } from 'react-native'
import PageLoader from '../../../components/PageLoader/PageLoader';
import SelectedChip from '../../../components/SeletedChip/SelectedChip';
import StateLoader from '../../../components/StateLoader/StateLoader';
import useRouter from '../../../libs/hook/useRouter';
import { paletteRed, primaryColor } from '../../../utils/color';
import useAskOrganizationsPage from './AskOrganizations.hook'
function AskOrganizations() {
    const { replace } = useRouter();
    const hook = useAskOrganizationsPage();
    return (
        <>
            <PageLoader loading={hook.loading} />
            <View style={{ justifyContent: 'center', alignItems: 'center', flex: 1 }}>
                <Text style={{ marginBottom: 10 }}>Bạn muốn theo dõi hội sách từ tổ chức nào ?</Text>
                <ScrollView contentContainerStyle={{ flexDirection: "row", flexWrap: "wrap", justifyContent: "center" }}
                    style={{ width: "95%", maxHeight: "30%", minHeight: "30%", marginBottom: 30 }}>
                    {
                        hook.data.organizationsSelect.map(item =>
                            <View style={{ margin: 5 }}>
                                <SelectedChip
                                    label={item.name}
                                    onPress={() => hook.event.onOrganizationsSeleted(item)}
                                    selected={hook.input.seletedOrganization.find(g => g == item.id) != undefined} />
                            </View>
                        )
                    }
                </ScrollView>
                <Input placeholder="Tìm kiếm tổ chức" />
                <Text style={{ color: "red", marginBottom: 20 }}>{hook.searchMessage}</Text>
                <View style={{ flexDirection: "row" }}>
                    <Button onPress={() => hook.event.onAskOrganizationsSubmit(true)} buttonStyle={{ borderRadius: 12, width: 120, height: 50, backgroundColor: paletteRed }}>
                        Bỏ qua
                    </Button>
                    <View style={{ marginLeft: 10, marginRight: 10 }} />

                    <Button
                        loading={hook.loading}
                        onPress={() => hook.event.onAskOrganizationsSubmit(false)} buttonStyle={{ borderRadius: 12, width: 120, height: 50, backgroundColor: primaryColor }}>
                        Xác nhận
                    </Button>
                </View>
            </View>
        </>
    )
}

export default AskOrganizations