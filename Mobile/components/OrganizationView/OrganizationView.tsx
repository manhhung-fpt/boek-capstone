import { View, Image, TouchableOpacity, Text, GestureResponderEvent, ActivityIndicator } from 'react-native'
import bookmark from "../../assets/icons/bookmark-border-white.png";
import bookmarkRemove from "../../assets/icons/bookmark-remove-white.png";
import { paletteRed, primaryTint1, primaryTint9, } from '../../utils/color';
import { OrganizationViewModel } from '../../objects/viewmodels/Organizations/OrganizationViewModel';
import { BasicOrganizationViewModel } from '../../objects/viewmodels/Organizations/BasicOrganizationViewModel';
import Shadow from '../Shadow/Shadow';
export interface OrganizationViewProps {
    organization: OrganizationViewModel | BasicOrganizationViewModel;
    tracked: boolean;
}
function OrganizationView(props: OrganizationViewProps) {
    return (
        <Shadow style={{
            backgroundColor: "white",
            flexDirection: "row",
            //height: 160,
            flex: 1,
            marginBottom: 20,
            borderRadius: 8
        }}>
            <View style={{
                width: "25%",
                alignItems: "center"
            }}>
                <Image style={{ height: "80%", width: "80%" }} source={{ uri: props.organization.imageUrl }} resizeMode={"contain"} />
            </View>
            <View style={{ width: "60%", overflow: "hidden", padding: 10, paddingTop: 20 }}>
                <Text style={{ fontSize: 18, fontWeight: "500" }}>{props.organization.name}</Text>
                <Text style={{ paddingTop: 5, fontSize: 15 }}>Địa chỉ: {props.organization.address}</Text>
                <Text style={{ paddingTop: 10 }}>Số điện thoại: {props.organization.phone}</Text>
            </View>
            <View style={{ width: "15%", justifyContent: "flex-start", alignItems: "center" }}>
                <View style={{ alignItems: "center", justifyContent: "center", width: "100%", height: "80%" }}>
                    {
                        props.tracked &&
                        <View
                            style={{
                                backgroundColor: primaryTint1,
                                borderRadius: 9999,
                                width: 45,
                                height: 45,
                                alignItems: "center",
                                justifyContent: "center"
                            }}>
                            <Image source={bookmark} resizeMode={"contain"} style={{ width: "60%", height: "60%" }} />
                        </View>
                    }
                </View>
            </View>
        </Shadow>
    )
}

export default OrganizationView