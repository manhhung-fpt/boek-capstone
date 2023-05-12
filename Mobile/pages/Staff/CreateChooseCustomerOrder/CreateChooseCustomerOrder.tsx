import React from 'react'
import { TouchableOpacity, View, Image, ScrollView } from 'react-native'
import { Text } from '@react-native-material/core'
import useCreateChooseCustomerOrderPage from './CreateChooseCustomerOrder.hook'
import PageLoader from '../../../components/PageLoader/PageLoader';
import range from '../../../libs/functions/range';
import { paletteGray, paletteGrayShade1, paletteGrayShade5, paletteGrayTint5, paletteGrayTint6, paletteGrayTint8, paletteGrayTint9, primaryTint1, primaryTint10, primaryTint7, primaryTint8, primaryTint9 } from '../../../utils/color';
import Shadow from '../../../components/Shadow/Shadow';
import avatar from "../../../assets/avatar.jpg";
import StickyHeaderSearchBar from '../../../components/StickyHeaderSearchBar/StickyHeaderSearchBar';
import Paging from '../../../components/Paging/Paging';
import { Button } from '@rneui/base';
import useRouter from '../../../libs/hook/useRouter';
import { StackScreenProps } from '@react-navigation/stack';
import { ParamListBase } from '@react-navigation/native';
import { StaffCampaignMobilesViewModel } from '../../../objects/viewmodels/Campaigns/StaffCampaignMobilesViewModel';

function CreateChooseCustomerOrder(props: StackScreenProps<ParamListBase>) {
    const params = props.route.params as { campaign: StaffCampaignMobilesViewModel, customer: {} };
    const { push } = useRouter();
    const hook = useCreateChooseCustomerOrderPage(props);
    return (
        <>
            <PageLoader loading={hook.ui.loading} />
            <ScrollView
                stickyHeaderIndices={[0]}
                stickyHeaderHiddenOnScroll
                style={{
                    backgroundColor: "white"
                }}>
                <StickyHeaderSearchBar
                    onChangeText={hook.input.name.set}
                    value={hook.input.name.value}
                    onSubmit={hook.event.onSearchSubmit}
                    placeHolder='Tìm kiếm khách hàng' />
                <View style={{
                    width: "100%",
                    alignItems: "center",
                    padding: 10,
                    height: "100%"
                }}>
                    {
                        hook.data.customers.map(item =>
                            <TouchableOpacity
                                onPress={() => hook.input.seletedCustomer.set(item)}
                                style={{
                                    //borderWidth: 1,
                                    width: "90%",
                                    borderRadius: 12,
                                    backgroundColor: item.id == hook.input.seletedCustomer.value?.id ? primaryTint9 : paletteGrayTint9,
                                    flexDirection: "row",
                                    marginBottom: 20,

                                    shadowColor: "#000",
                                    shadowOffset: {
                                        width: 0,
                                        height: 12,
                                    },
                                    shadowOpacity: 0.58,
                                    shadowRadius: 16.00,
                                    elevation: 8
                                }}>
                                <Shadow style={{
                                    //borderWidth: 1,
                                    width: "20%",
                                    alignItems: "center",
                                    justifyContent: "center",
                                    elevation: 2
                                }}>
                                    <Image source={{ uri: item.imageUrl }} resizeMode="contain" style={{ width: 50, height: 50, borderRadius: 999 }} />
                                </Shadow>
                                <View style={{
                                    //borderWidth: 1,
                                    width: "80%",
                                    rowGap: 5,
                                    padding: 10
                                }}>
                                    <Text style={{ fontWeight: "500" }}>Tên: {item.name}</Text>
                                    <Text style={{ color: paletteGrayShade5 }}>SĐT: {item.phone}</Text>
                                    <Text style={{ color: paletteGrayShade5 }}>Địa chỉ: {item.address}</Text>
                                </View>
                            </TouchableOpacity>
                        )
                    }
                    <Paging
                        currentPage={hook.paging.currentPage}
                        maxPage={hook.paging.maxPage}
                        onPageNavigation={hook.paging.onPageNavigation} />
                </View>
            </ScrollView>
            <Shadow style={{
                display: hook.input.seletedCustomer.value && hook.input.seletedCustomer.value.id != "" ? "flex" : "none",
                backgroundColor: "white",
                height: "10%",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <Button
                    onPress={() => push("CreateChooseProductsOrder", {
                        campaign: params.campaign,
                        customer: {
                            id: hook.input.seletedCustomer.value?.id,
                            name: hook.input.seletedCustomer.value?.name,
                            phone: hook.input.seletedCustomer.value?.phone,
                            email: hook.input.seletedCustomer.value?.email
                        }
                    })}
                    buttonStyle={{
                        backgroundColor: primaryTint1,
                        width: 150,
                        height: 50,
                        shadowColor: "#000",
                        shadowOffset: {
                            width: 0,
                            height: 12,
                        },
                        shadowOpacity: 0.58,
                        shadowRadius: 16.00,
                        elevation: 24
                    }}>
                    Tiếp theo
                </Button>
            </Shadow>
        </>
    )
}

export default CreateChooseCustomerOrder
