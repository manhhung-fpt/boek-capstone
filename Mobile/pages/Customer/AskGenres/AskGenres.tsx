import { Button, Input } from '@rneui/themed'
import React from 'react'
import { View, Text, ScrollView } from 'react-native'
import PageLoader from '../../../components/PageLoader/PageLoader';
import SelectedChip from '../../../components/SeletedChip/SelectedChip';
import useRouter from '../../../libs/hook/useRouter';
import { paletteRed, primaryColor } from '../../../utils/color';
import useAskGenrePage from './AskGenres.hook'
export  interface AskGenresProps {
    skiped?: boolean;
}
function AskGenres(props: AskGenresProps) {
    const { replace } = useRouter();
    const hook = useAskGenrePage(props);
    return (
        <View style={{ justifyContent: 'center', alignItems: 'center', flex: 1 }}>
            <Text style={{ marginBottom: 10 }}>Bạn muốn tham gia nhóm nào ?</Text>
            <PageLoader loading={hook.loading}/>
            <ScrollView contentContainerStyle={{ flexDirection: "row", flexWrap: "wrap", justifyContent: "center" }}
                style={{ width: "95%", maxHeight: "30%", minHeight: "30%", marginBottom: 30 }}>
                {
                    hook.data.groupsSelect.map(item =>
                        <View style={{ margin: 5 }}>
                            <SelectedChip
                                label={item.name}
                                onPress={() => hook.event.onGroupsSelected(item)}
                                selected={hook.input.selectedGroups.find(g => g == item.id) != undefined} />
                        </View>
                    )
                }
            </ScrollView>
            <Input placeholder="Tìm kiếm nhóm" value={hook.input.search.value} onChangeText={hook.input.search.set} />
            <Text style={{ color: "red", marginBottom: 20 }}>{hook.searchMessage}</Text>

            <View style={{ flexDirection: "row" }}>
                {/* {
                    props.skiped &&
                    <Button onPress={() => hook.event.onAskGenresSubmit(true)} buttonStyle={{ borderRadius: 12, width: 120, height: 50, backgroundColor: paletteRed }}>
                        Bỏ qua
                    </Button>
                } */}
                <View style={{ marginLeft: 10, marginRight: 10 }} />
                <Button
                    onPress={() => hook.event.onAskGenresSubmit(false)}
                    buttonStyle={{
                        width: 120,
                        height: 50,
                        borderRadius: 12,
                        backgroundColor: primaryColor
                    }}>
                    Xác nhận
                </Button>
            </View>

        </View>
    )
}

export default AskGenres