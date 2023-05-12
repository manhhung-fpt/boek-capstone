import React from 'react'
import { View, Image, ScrollView, TouchableOpacity, TextInput, Text } from 'react-native'
import SelectDropdown from 'react-native-select-dropdown'
import { getMessage } from '../../../utils/Validators'
import { District } from '../../../objects/enums/Districts'
import { Button } from '@rneui/base'
import editIcon from "../../../assets/icons/edit.png";
import useAppContext from '../../../context/Context'
import useStaffPersonalInformationPage from './StaffPersonalInformation.hook'
import { Province } from '../../../objects/enums/Province'
import { Ward } from '../../../objects/enums/Ward'
import PageLoader from '../../../components/PageLoader/PageLoader'

function StaffPersonalInformation() {
    const hook = useStaffPersonalInformationPage();
    const { user } = useAppContext();
    return (
        <>
            <PageLoader loading={hook.loading} opacity={hook.opacity} />
            <View>
                <View style={{
                    backgroundColor: "#1E293B",
                    justifyContent: 'center',
                    height: 160
                }}>
                    <View style={{
                        justifyContent: "center",
                        alignItems: "center"
                    }}>
                        <View style={{
                            borderWidth: 2,
                            borderColor: "#064E3B",
                            height: 90,
                            width: 90,
                            borderRadius: 9999,
                            overflow: "hidden"
                        }}>
                            <Image source={{ uri: user?.imageUrl }} style={{ width: "100%", height: "100%" }} resizeMode="contain" />
                        </View>
                    </View>
                </View>

                <ScrollView style={{
                }}>

                    <View style={{ flexDirection: "row", maxWidth: "100%", height: 60 }}>
                        <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                            <Text>Email:</Text>
                        </View>
                        <View style={{ width: "70%", height: "100%", alignItems: "flex-end", justifyContent: "center", paddingRight: 20 }}>
                            <Text>{hook.data.email}</Text>
                        </View>
                    </View>

                    <View style={{ flexDirection: "row", maxWidth: "100%", height: 60 }}>
                        <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                            <Text>Họ và tên:</Text>
                        </View>
                        <View style={{ width: "60%", height: "100%", alignItems: "flex-end", justifyContent: "center", paddingRight: 20 }}>
                            <TextInput
                                ref={hook.ref.inputNameRef}
                                placeholder='Chưa có thông tin'
                                value={hook.input.name.value}
                                onChangeText={hook.input.name.set}
                                style={{ textAlign: "right" }}
                            />
                            <Text style={{ color: "red" }}>{getMessage(hook.validator, "name")}</Text>
                        </View>
                        <View style={{ width: "10%", height: "80%", alignItems: "flex-start", justifyContent: "center" }}>
                            <TouchableOpacity onPress={() => hook.ref.inputNameRef.current?.focus()}>
                                <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                            </TouchableOpacity>
                        </View>
                    </View>

                    <View style={{ flexDirection: "row", maxWidth: "100%", height: 70 }}>
                        <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                            <Text>Tỉnh:</Text>
                        </View>
                        <View style={{ width: "60%", height: "100%", alignItems: "flex-end", justifyContent: "center", paddingRight: 20 }}>
                            <View style={{ alignItems: "flex-end", height: "55%" }}>
                                <SelectDropdown
                                    defaultValueByIndex={hook.data.provincesSelect.findIndex(p => p.code == hook.input.province.value?.code)}
                                    ref={hook.ref.inputProvinceRef}
                                    renderDropdownIcon={() => <></>}
                                    buttonStyle={{ width: "100%", justifyContent: "flex-end" }}
                                    buttonTextStyle={{ fontSize: 14, textAlign: "right", color: "black" }}
                                    defaultButtonText="Chọn địa điểm"
                                    onChangeSearchInputText={() => { console.log("Hello") }}
                                    data={hook.data.provincesSelect}
                                    onSelect={(selectedItem, index) => hook.event.onProvinceSelected(selectedItem)}
                                    buttonTextAfterSelection={(selectedItem, index) => {
                                        return (selectedItem as Province).nameWithType
                                    }}
                                    rowTextForSelection={(item, index) => {
                                        return (item as Province).nameWithType
                                    }}
                                />
                            </View>
                            <Text style={{ color: "red" }}>{getMessage(hook.validator, "province")}</Text>
                        </View>
                        <View style={{ width: "10%", height: "80%", alignItems: "flex-start", justifyContent: "center" }}>
                            <TouchableOpacity onPress={() => hook.ref.inputProvinceRef.current?.openDropdown()}>
                                <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                            </TouchableOpacity>
                        </View>
                    </View>

                    <View style={{ flexDirection: "row", maxWidth: "100%", height: 70 }}>
                        <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                            <Text>Quận:</Text>
                        </View>
                        <View style={{ width: "60%", height: "100%", alignItems: "flex-end", justifyContent: "center", paddingRight: 20 }}>
                            <View style={{ alignItems: "flex-end", height: "55%" }}>
                                <SelectDropdown
                                    onFocus={hook.event.onDistrictSelectedFocus}
                                    ref={hook.ref.inputDistrictRef}
                                    defaultValue={hook.data.districtSelect.find(p => p.code == hook.input.district.value?.code)}
                                    renderDropdownIcon={() => <></>}
                                    buttonStyle={{ width: "100%", justifyContent: "flex-end" }}
                                    buttonTextStyle={{ fontSize: 14, textAlign: "right", color: "black" }}
                                    defaultButtonText="Chọn địa điểm"
                                    onChangeSearchInputText={() => { console.log("Hello") }}
                                    data={hook.data.districtSelect}
                                    onSelect={(selectedItem, index) => hook.event.onDistrictSelected(selectedItem)}
                                    buttonTextAfterSelection={(selectedItem, index) => {
                                        return (selectedItem as District).nameWithType
                                    }}
                                    rowTextForSelection={(item, index) => {
                                        return (item as District).nameWithType
                                    }}
                                />
                            </View>
                            <Text style={{ color: "red" }}>{getMessage(hook.validator, "district")}</Text>
                        </View>
                        <View style={{ width: "10%", height: "80%", alignItems: "flex-start", justifyContent: "center" }}>
                            <TouchableOpacity onPress={() => hook.ref.inputDistrictRef.current?.openDropdown()}>
                                <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                            </TouchableOpacity>
                        </View>
                    </View>

                    <View style={{ flexDirection: "row", maxWidth: "100%", height: 70 }}>
                        <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                            <Text>Phường:</Text>
                        </View>
                        <View style={{ width: "60%", height: "100%", alignItems: "flex-end", justifyContent: "center", paddingRight: 20 }}>
                            <View style={{ alignItems: "flex-end", height: "55%" }}>
                                <SelectDropdown
                                    onFocus={hook.event.onWardSelectedFocus}
                                    ref={hook.ref.inputWardRef}
                                    defaultValue={hook.data.wardSelect.find(p => p.code == hook.input.ward.value?.code)}
                                    renderDropdownIcon={() => <></>}
                                    buttonStyle={{ width: "100%", justifyContent: "flex-end" }}
                                    buttonTextStyle={{ fontSize: 14, textAlign: "right", color: "black" }}
                                    defaultButtonText="Chọn địa điểm"
                                    onChangeSearchInputText={() => { console.log("Hello") }}
                                    data={hook.data.wardSelect}
                                    onSelect={(selectedItem, index) => hook.event.onWardSelected(selectedItem)}
                                    buttonTextAfterSelection={(selectedItem, index) => {
                                        return (selectedItem as Ward).nameWithType
                                    }}
                                    rowTextForSelection={(item, index) => {
                                        return (item as Ward).nameWithType
                                    }}
                                />
                            </View>
                            <Text style={{ color: "red" }}>{getMessage(hook.validator, "ward")}</Text>
                        </View>
                        <View style={{ width: "10%", height: "80%", alignItems: "flex-start", justifyContent: "center" }}>
                            <TouchableOpacity onPress={() => hook.ref.inputWardRef.current?.openDropdown()}>
                                <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                            </TouchableOpacity>
                        </View>
                    </View>

                    <View style={{ flexDirection: "row", maxWidth: "100%", height: 60 }}>
                        <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                            <Text>Địa chỉ:</Text>
                        </View>
                        <View style={{ width: "60%", height: "100%", alignItems: "flex-end", justifyContent: "center", paddingRight: 20 }}>
                            <TextInput
                                ref={hook.ref.inputAddressRef}
                                placeholder='Chưa có thông tin'
                                value={hook.input.address.value}
                                onChangeText={hook.input.address.set}
                                style={{ textAlign: "right" }}
                            />
                            <Text style={{ color: "red" }}>{getMessage(hook.validator, "address")}</Text>
                        </View>
                        <View style={{ width: "10%", height: "80%", alignItems: "flex-start", justifyContent: "center" }}>
                            <TouchableOpacity onPress={() => hook.ref.inputAddressRef.current?.focus()}>
                                <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                            </TouchableOpacity>
                        </View>
                    </View>

                    <View style={{ flexDirection: "row", maxWidth: "100%", height: 70 }}>
                        <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                            <Text>Số điện thoại:</Text>
                        </View>
                        <View style={{ width: "60%", height: "100%", alignItems: "flex-end", justifyContent: "center", paddingRight: 20 }}>
                            <TextInput
                                placeholder="Chưa có thông tin"
                                ref={hook.ref.inputPhoneRef}
                                keyboardType="numeric"
                                value={hook.input.phone.value}
                                onChangeText={hook.input.phone.set}
                                style={{ textAlign: "right" }}
                            />
                            <Text style={{ color: "red" }}>{getMessage(hook.validator, "phone")}</Text>
                        </View>
                        <View style={{ width: "10%", height: "80%", alignItems: "flex-start", justifyContent: "center" }}>
                            <TouchableOpacity onPress={() => hook.ref.inputPhoneRef.current?.focus()}>
                                <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
                            </TouchableOpacity>
                        </View>
                    </View>
                    {
                        hook.buttonShowed &&
                        <View style={{ alignItems: "center" }}>
                            <Button onPress={hook.event.onSubmit} buttonStyle={{ marginTop: 5, height: 40, borderRadius: 24, minWidth: 224, minHeight: 56, backgroundColor: "#3730A3" }}>Lưu thay đổi</Button>
                        </View>
                    }
                </ScrollView>
            </View>
        </>
    )
}

export default StaffPersonalInformation