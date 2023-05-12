import { StyleSheet, View, Text, Image, TouchableOpacity, Pressable } from 'react-native';
import useProfilePage from './Profile.hook';
import { Button } from '@rneui/base';
import accountWhite from "../../../assets/icons/account-circle-white.png";
import { BottomTabScreenProps } from "@react-navigation/bottom-tabs";
import { ParamListBase } from '@react-navigation/native';
import useAuth from '../../../libs/hook/useAuth';
import AuthorizeView from '../../../libs/AuthorizeView';
import NonAuthorizeView from '../../../libs/NonAuthorizeView';
import GoogleLoginButton from '../../../components/GoogleLoginButton/GoogleLoginButton';
import TouchCard from '../../../components/TouchCard/TouchCard';
import { paletteGray, paletteGrayLight, paletteGrayTint6, paletteGrayTint8, primaryTint1 } from '../../../utils/color';
import { Role } from '../../../objects/enums/Role';
import PageLoader from '../../../components/PageLoader/PageLoader';
import useAppContext from '../../../context/Context';
import Info from '../../../assets/SvgComponents/Info';
import useRouter from '../../../libs/hook/useRouter';
import LayoutModal from '../../../components/LayoutModal/LayoutModal';
export interface ProfileProps extends BottomTabScreenProps<ParamListBase> {

}
function Profile(props: ProfileProps) {
  const hook = useProfilePage(props);
  const { authenticated } = useAuth();
  const { navigate } = useRouter();
  const { user } = useAppContext();
  return (
    <>
      <LayoutModal visible={hook.ui.levelModalShowed} onClose={() => hook.ui.setLevelModalShowed(false)}>
        <Pressable
          onPress={() => hook.ui.setLevelModalShowed(false)}
          style={{
            width: "100%",
            height: "100%",
            alignItems: "center",
            justifyContent: "center",
            backgroundColor: "rgba(0,0,0,0.6)"
          }}>
          <View style={{
            backgroundColor: "white",
            width: 320,
            borderRadius : 8
          }}>
            {
              hook.data.levels.map(item =>
                <View style={{
                  //borderWidth: 1,
                  height: 90,
                  width: "100%",
                  flexDirection: "row"
                }}>
                  <View style={{
                    //borderWidth: 1,
                    width: "30%",
                    height: "100%",
                    alignItems: "center",
                    justifyContent: 'center'
                  }}>
                    <View style={{
                      backgroundColor: paletteGrayTint6,
                      //borderWidth: 1,
                      borderRadius: 999,
                      width: 60,
                      height: 60,
                      justifyContent: "center",
                      alignItems: "center",
                    }}>
                      <Text style={{ fontSize: 18 }}>{item.name[0].toLocaleUpperCase()}</Text>
                    </View>
                  </View>
                  <View style={{
                    //borderWidth: 1,
                    width: "70%",
                    justifyContent: "center",
                    rowGap: 5
                  }}>
                    <Text style={{ fontSize: 16 }}>{item.name}</Text>
                    <Text style={{ fontSize: 16 }}>Số điểm: {item.conditionalPoint}</Text>
                  </View>
                </View>
              )
            }

          </View>
        </Pressable>
      </LayoutModal>
      <View style={{ flex: 1 }}>
        <View style={{
          backgroundColor: "#1E293B",
          justifyContent: 'center',
          height: 176
        }}>
          <View style={styles.headerOuter}>
            <View style={{
              minWidth: "30%",
              flex: 1,
              justifyContent: "center",
              alignItems: "center"
            }}>
              <View style={{
                borderWidth: 2,
                borderColor: "#064E3B",
                height: 113,
                width: 113,
                borderRadius: 9999,
                overflow: "hidden"
              }}>
                <Image source={authenticated ? { uri: user?.imageUrl } : accountWhite} style={{
                  width: "100%",
                  height: "100%"
                }} resizeMode="cover" />
              </View>
            </View>

            <View style={{
              minWidth: "75%",
              flex: 1,
              justifyContent: "center",
              alignItems: "center"
            }}>
              <View style={{ minWidth: "87%" }}>
                <AuthorizeView>
                  <Text style={styles.infoName}>{user?.name}</Text>
                  <Text style={{ color: "white" }}>{user?.email}</Text>
                  <View style={{ marginTop: 10 }}></View>
                  <AuthorizeView roles={[Role.customer.toString()]}>
                    <View style={{
                      //borderWidth: 1,
                      flexDirection: "row"
                    }}>
                      <View style={{

                        width: "60%"
                      }}>
                        <Text style={{ color: "white", fontWeight: "600" }}>Cấp độ: {hook.data.customer && hook.data.customer?.level.name}</Text>
                        <Text style={{ color: "white", fontWeight: "600" }}>Số điểm: {hook.data.customer && hook.data.customer?.point}</Text>
                      </View>
                      <View style={{ justifyContent: "center" }}>
                        <TouchableOpacity onPress={() => hook.ui.setLevelModalShowed(true)}>
                          <Info fill={"white"} />
                        </TouchableOpacity>
                      </View>
                    </View>
                  </AuthorizeView>
                  <AuthorizeView roles={[Role.staff.toString()]}>

                  </AuthorizeView>
                </AuthorizeView>
                <NonAuthorizeView>
                  <Text style={{ color: "white", fontSize: 16, marginBottom: 10 }}>Chào mừng bạn đến với Boek</Text>
                  <View style={{ width: "50%" }} >
                    {/* <GoogleLoginButton onPress={hook.event.onLogin} /> */}
                  </View>
                </NonAuthorizeView>
              </View>
            </View>
          </View>
        </View>

        <AuthorizeView roles={[Role.staff.toString()]}>
          <TouchCard label="Thông tin cá nhân" onPress={async () => navigate("StaffPersonalInformation")} />
          {/* <TouchCard label="Đơn hàng" onPress={async () => navigate("StaffOrders")} /> */}
        </AuthorizeView>
        <AuthorizeView roles={[Role.customer.toString()]}>
          <TouchCard label="Thông tin cá nhân" onPress={async () => navigate("PersonalInformation")} />
          <TouchCard label="Đơn hàng" onPress={async () => navigate("Orders")} />
          <TouchCard label="Nhóm" onPress={async () => navigate("AskGenres")} />
          <TouchCard label="Tổ chức" onPress={async () => navigate("Organizations")} />
          {/* <TouchCard label="Sách của tôi" /> */}
        </AuthorizeView>
        <NonAuthorizeView>
          <TouchCard label="Thông tin cá nhân" onPress={async () => navigate("PersonalInformation")} />
          <TouchCard label="Đơn hàng" onPress={async () => navigate("Orders")} />
          <TouchCard label="Nhóm" onPress={async () => navigate("AskGenres")} />
          <TouchCard label="Tổ chức" onPress={async () => navigate("Organizations")} />
        </NonAuthorizeView>
        <AuthorizeView>
          <View style={{ width: "100%", paddingTop: 100, alignItems: "center", justifyContent: "center" }}>
            <Button
              onPress={async () => await hook.event.logout(true)}
              buttonStyle={{
                height: 40,
                borderRadius: 24,
                minWidth: 224,
                minHeight: 56,
                backgroundColor: primaryTint1
              }}>Đăng xuất</Button>
          </View>
        </AuthorizeView>
      </View >
    </>
  )
}

const styles = StyleSheet.create({
  headerOuter: {
    minHeight: 96,
    marginTop: 20,
    marginLeft: 8,
    marginRight: 8,
    flex: 1,
    flexDirection: "row"
  },
  infoName: {
    color: "white",
    fontWeight: "600",
    fontSize: 18,
    lineHeight: 28
  },
  space: {
    minHeight: 80
  }
});
//text-white font-semibold text-lg
export default Profile