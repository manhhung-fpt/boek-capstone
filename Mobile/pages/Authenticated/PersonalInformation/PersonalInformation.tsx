import { Button, CheckBox } from '@rneui/themed';
import { View, Text, TextInput, StyleSheet, Image, TouchableOpacity, ScrollView } from 'react-native';
import editIcon from "../../../assets/icons/edit.png";
import usePersonalInformationPage from './PersonalInformation.hook';
import DateTimePickerInput from "../../../components/DateTimePickerInput/DateTimePickerInput";
import AuthorizeView from "../../../libs/AuthorizeView";
import { Role } from "../../../objects/enums/Role";
import { getMessage } from '../../../utils/Validators';
import PageLoader from '../../../components/PageLoader/PageLoader';
import SelectDropdown from 'react-native-select-dropdown';
import useAppContext from '../../../context/Context';
import { Province } from '../../../objects/enums/Province';
import { District } from '../../../objects/enums/Districts';
import { Ward } from '../../../objects/enums/Ward';

function PersonalInformation() {
  const hook = usePersonalInformationPage();
  const { user } = useAppContext();
  return (
    <>
      <PageLoader loading={hook.loading} opacity={hook.opacity} />
      <View>
        <View style={{
          backgroundColor: "#1E293B",
          justifyContent: 'center',
          height: 120
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
                style={{ textAlign: "right" }} />
              <Text style={{ color: "red" }}>{getMessage(hook.validator, "name")}</Text>
            </View>
            <View style={{ width: "10%", height: "80%", alignItems: "flex-start", justifyContent: "center" }}>
              <TouchableOpacity onPress={() => hook.ref.inputNameRef.current?.focus()}>
                <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />
              </TouchableOpacity>
            </View>
          </View>

          <AuthorizeView roles={[Role.customer.toString()]}>
            <View style={{ flexDirection: "row", maxWidth: "100%", height: 60 }}>
              <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                <Text>Giới tính:</Text>
              </View>
              <View style={{ width: "70%", height: "100%", alignItems: "flex-end", justifyContent: "flex-end", flexDirection: "row" }}>
                <CheckBox
                  title="Nữ"
                  checked={!hook.input.gender.value}
                  center
                  checkedIcon="dot-circle-o"
                  onPress={() => hook.input.gender.set(false)}
                  uncheckedIcon="circle-o"
                  containerStyle={{ backgroundColor: "transparent" }} />
                <CheckBox
                  title="Nam"
                  checked={hook.input.gender.value}
                  center
                  checkedIcon="dot-circle-o"
                  uncheckedIcon="circle-o"
                  onPress={() => hook.input.gender.set(true)}
                  containerStyle={{ backgroundColor: "transparent" }} />
              </View>
            </View>

            <View style={{ flexDirection: "row", maxWidth: "100%", height: 60 }}>
              <View style={{ width: "30%", height: "100%", alignItems: "flex-start", justifyContent: "center", paddingLeft: 10 }}>
                <Text>Ngày sinh:</Text>
              </View>
              <View style={{ width: "70%", height: "70%" }}>
                <DateTimePickerInput
                  value={hook.input.birth.value}
                  onConfirm={hook.input.birth.set}
                  maximumDate={new Date()}
                  icon={() => <Image source={editIcon} style={{ maxHeight: 25, maxWidth: 25 }} />}
                  hideReset />
                <View style={{ width: "80%", alignItems: "flex-end" }}>
                  <Text style={{ color: "red" }}>{getMessage(hook.validator, "dob")}</Text>
                </View>
              </View>
            </View>
          </AuthorizeView>

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
                style={{ textAlign: "right" }} />
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
                style={{ textAlign: "right" }} />
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
const styles = StyleSheet.create({
  label: {
    borderWidth: 1,
    width: "30%",
    height: "100%",
    alignItems: "flex-start",
    justifyContent: "flex-start",
    paddingLeft: 10,
    paddingTop: 5
  },
  inputContainer: {
    //borderWidth: 1,
    width: "60%",
    height: "100%",
    alignItems: "flex-end",
    justifyContent: "flex-start",
    paddingRight: 20
  },
  imageContainer: {
    //borderWidth: 1,
    width: "100%",
    height: "100%",
    alignItems: "flex-start",
    justifyContent: "flex-start",
  },
  validationMessage:
  {
    color: "red"
  }
});


export default PersonalInformation