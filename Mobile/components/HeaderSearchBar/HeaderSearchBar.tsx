import { Input } from '@rneui/base';
import React, { PropsWithChildren, useEffect} from 'react'
import { TouchableOpacity, View, Image, SafeAreaView } from 'react-native'
import searchWhite from "../../assets/icons/search-white.png";
import arrowBackWhite from "../../assets/icons/arrow-back-white.png";
import useRouter from '../../libs/hook/useRouter';
import { paletteGray, primaryColor } from '../../utils/color';

interface HeaderSearchBarProps extends PropsWithChildren {
    value?: string;
    onChangeText?: (e: string) => void;
    onSubmit?: () => void;
}
function HeaderSearchBar(props: HeaderSearchBarProps) {
    const { canGoBack, goBack } = useRouter();
    useEffect(() => {
        console.log(props.onSubmit);
    },[props.onSubmit]);
    return (
        <SafeAreaView style={{ backgroundColor: primaryColor }}>
            <View style={{
                height: 60,
                width: "98%",
                flexDirection: "row",
            }}>
                <View
                    style={{
                        //borderWidth: 1,
                        width: canGoBack() ? "10%" : 0,
                        height: "100%"
                    }}>
                    <TouchableOpacity
                        style={{
                            alignItems: "center",
                            justifyContent: "center",
                            width: "100%",
                            height: "100%"
                        }}
                        onPress={goBack}>
                        <Image source={arrowBackWhite} resizeMode="contain" style={{ width: "100%", height: "40%" }} />
                    </TouchableOpacity>
                </View>
                <View style={{ width: canGoBack() ? "80%" : "90%" }}>
                    <Input
                        onSubmitEditing={props.onSubmit}
                        value={props.value}
                        onChangeText={props.onChangeText}
                        placeholderTextColor={paletteGray}
                        placeholder="Tìm kiếm"
                        style={{
                            //borderWidth: 1,
                            color: "white",
                            fontSize: 16,
                            borderBottomWidth: 1,
                            borderBottomColor: "white"
                        }} />
                </View>
                <View style={{ width: "10%", paddingTop: 5 }}>
                    <TouchableOpacity onPress={props.onSubmit} style={{ height: "100%" }}>
                        <Image source={searchWhite} style={{ width: 35 }} resizeMode="contain" />
                    </TouchableOpacity>
                </View>
            </View>
        </SafeAreaView>
    )
}

export default HeaderSearchBar