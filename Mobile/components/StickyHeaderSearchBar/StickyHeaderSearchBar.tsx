import { Input } from '@rneui/base'
import React from 'react'
import { TouchableOpacity, View, Image } from 'react-native'
import { paletteGray, paletteGrayLight, paletteGrayTint8 } from '../../utils/color';
import searchWhite from "../../assets/icons/search-black.png";

interface StickyHeaderSearchBarProps {
    placeHolder?: string;
    value?: string;
    onChangeText?: (e: string) => void;
    onSubmit?: () => void;
}
function StickyHeaderSearchBar(props: StickyHeaderSearchBarProps) {
    return (
        <View style={{
            height: 70,
            width: "100%",
            alignItems: "center",
            justifyContent: "center"
        }}>
            <View style={{
                borderWidth: 0.5,
                borderColor: paletteGrayLight,
                borderRadius: 24,
                backgroundColor: "white",
                width: "92%",
                height: "80%",

                shadowColor: "#000",
                shadowOffset: {
                    width: 0,
                    height: 12,
                },
                shadowOpacity: 0.58,
                shadowRadius: 16.00,
                elevation: 30
            }}>
                <View style={{ flexDirection: "row" }}>
                    <View style={{ width: "5%" }} />
                    <View style={{
                        width: "90%",
                        flexDirection: "row",
                    }}>
                        <View style={{ width: "90%" }}>
                            <Input
                                onSubmitEditing={props.onSubmit}
                                value={props.value}
                                onChangeText={props.onChangeText}
                                placeholderTextColor={paletteGray}
                                placeholder={props.placeHolder || "Tìm kiếm"}
                                style={{
                                    //borderWidth: 1,
                                    color: "black",
                                    fontSize: 16,
                                    borderBottomWidth: 1,
                                    borderBottomColor: "white"
                                }} />
                        </View>
                        <View style={{ width: "10%", paddingTop: 5 }}>
                            <TouchableOpacity onPress={props.onSubmit} style={{ height: "100%" }}>
                                <Image source={searchWhite} style={{ width: 25 }} resizeMode="contain" />
                            </TouchableOpacity>
                        </View>
                    </View>
                </View>
            </View>
        </View>
    )
}

export default StickyHeaderSearchBar