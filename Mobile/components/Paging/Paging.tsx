import React from 'react'
import { View, Text, Image, TouchableOpacity, ViewStyle, StyleProp } from 'react-native'
import { primaryColor, primaryTint3 } from '../../utils/color';
import navigateBack from "../../assets/icons/navigate-left-white.png";
import navigateNext from "../../assets/icons/navigate-right-white.png";
import usePagingComponent from './Paging.hook';
import range from '../../libs/functions/range';
export interface PagingProps {
    style?: StyleProp<ViewStyle>;
    maxPage: number;
    currentPage: number;
    onPageNavigation?: (page: number) => void | Promise<void>;
}
function Paging(props: PagingProps) {
    const hook = usePagingComponent(props);
    return (
        <View style={props.style}>
            <View style={{
                flexDirection: "row",
                width: "100%",
                height: 60,
                alignItems: "center",
                justifyContent: "center"
            }}>
                <TouchableOpacity
                    onPress={() => props.onPageNavigation && props.onPageNavigation(props.currentPage - 1)}
                    style={{
                        display: props.currentPage > 1 ? "flex" : "none",
                        backgroundColor: primaryColor,
                        width: 35,
                        height: 35,
                        borderRadius: 9999,
                        alignItems: "center",
                        justifyContent: "center",
                        margin: 5
                    }}>
                    <Image style={{ width: "60%", height: "60%" }} source={navigateBack} resizeMode="contain" />
                </TouchableOpacity>
                <TouchableOpacity
                    onPress={() => props.onPageNavigation && props.onPageNavigation(1)}
                    style={{
                        display: hook.showStartDot ? "flex" : "none",
                        backgroundColor: primaryTint3,
                        width: 30,
                        height: 30,
                        borderRadius: 9999,
                        alignItems: "center",
                        justifyContent: "center"
                    }}>
                    <Text style={{ fontSize: 12, color: "white" }}>1</Text>
                </TouchableOpacity>
                <ThreeDot display={hook.showStartDot} />
                {
                    range(hook.startPage, hook.endPage).map(item =>
                        item == props.currentPage ?
                            <View
                                key={Math.random()}
                                style={{
                                    backgroundColor: primaryColor,
                                    width: 40,
                                    height: 40,
                                    borderRadius: 9999,
                                    alignItems: "center",
                                    justifyContent: "center",
                                    margin: 4
                                }}>
                                <Text style={{ fontSize: 12, color: "white" }}>{item}</Text>
                            </View>
                            :
                            <TouchableOpacity
                                onPress={() => props.onPageNavigation && props.onPageNavigation(item)}
                                key={Math.random()}
                                style={{
                                    backgroundColor: primaryTint3,
                                    width: 30,
                                    height: 30,
                                    borderRadius: 9999,
                                    alignItems: "center",
                                    justifyContent: "center",
                                    margin: 5
                                }}>
                                <Text style={{ fontSize: 12, color: "white" }}>{item}</Text>
                            </TouchableOpacity>
                    )
                }
                <ThreeDot display={hook.showEndDot} />
                <TouchableOpacity
                    onPress={() => props.onPageNavigation && props.onPageNavigation(props.maxPage)}
                    style={{
                        display: hook.showEndDot ? "flex" : "none",
                        backgroundColor: primaryTint3,
                        width: 30,
                        height: 30,
                        borderRadius: 9999,
                        alignItems: "center",
                        justifyContent: "center"
                    }}>
                    <Text style={{ fontSize: 12, color: "white" }}>{props.maxPage}</Text>
                </TouchableOpacity>
                <TouchableOpacity
                    onPress={() => props.onPageNavigation && props.onPageNavigation(props.currentPage + 1)}
                    style={{
                        display: props.currentPage < props.maxPage ? "flex" : "none",
                        backgroundColor: primaryColor,
                        width: 35,
                        height: 35,
                        borderRadius: 9999,
                        alignItems: "center",
                        justifyContent: "center",
                        margin: 5
                    }}>
                    <Image style={{ width: "60%", height: "60%" }} source={navigateNext} resizeMode="contain" />
                </TouchableOpacity>
            </View>
        </View>
    )
}
function ThreeDot(props: { display: boolean }) {
    return (
        <View style={{
            display: props.display ? "flex" : "none",
            height: 20,
            width: 20,
            alignItems: "flex-end",
            justifyContent: "center",
            flexDirection: "row"
        }}>
            <View style={{ backgroundColor: primaryColor, width: 3, height: 3, borderRadius: 9999, marginRight: 2 }} />
            <View style={{ backgroundColor: primaryColor, width: 3, height: 3, borderRadius: 9999, marginRight: 2 }} />
            <View style={{ backgroundColor: primaryColor, width: 3, height: 3, borderRadius: 9999, marginRight: 2 }} />
        </View>
    )
}

export default Paging