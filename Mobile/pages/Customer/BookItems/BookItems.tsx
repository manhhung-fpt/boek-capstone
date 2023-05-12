import { ParamListBase } from '@react-navigation/native'
import { StackScreenProps } from '@react-navigation/stack'
import React from 'react'
import { TouchableOpacity, View, Image, Text } from 'react-native'
import useRouter from '../../../libs/hook/useRouter'
import { primaryTint7 } from '../../../utils/color'
import useBookItemsPage from './BookItems.hook';
import { Ionicons } from '@expo/vector-icons';
import truncateString from '../../../libs/functions/truncateString'


function BookItems(props: StackScreenProps<ParamListBase>) {
    const { push } = useRouter();
    const hook = useBookItemsPage(props);
    return (
        <View style={{
            flexDirection: "row",
            padding: 10
        }}>
            {
                hook.data.bookProduct?.bookProductItems?.map(item =>
                    <View
                        style={{
                            width: 195,
                            height: 240,
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                        <TouchableOpacity
                            onPress={() => push("BookItemDetail", { data: item })}
                            style={{
                                borderColor: primaryTint7,
                                borderWidth: 1,
                                borderRadius: 8,
                                height: "97%",
                                width: "95%"
                            }}>
                            <View
                                style={{
                                    height: "60%",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}>
                                <View
                                    style={{
                                        height: "95%",
                                        width: "95%"
                                    }}>
                                    <Image
                                        source={{ uri: item.book?.imageUrl }}
                                        resizeMode="cover"
                                        style={{ height: "100%", width: "100%" }} />
                                </View>
                            </View>
                            <View
                                style={{
                                    width: "100%",
                                    height: "40%",
                                    alignItems: "center",
                                    justifyContent: "center"
                                }}>
                                <View style={{
                                    width: "95%",
                                    height: "90%"
                                }}>
                                    <View style={{ height: "40%", paddingLeft: 2 }}>
                                        <Text style={{ fontSize: 16, fontWeight: "600" }}>{truncateString(item.book?.name, 3)}</Text>
                                    </View>
                                    <View style={{ height: "60%", width: "100%", flexDirection: "row", columnGap: 5 }} >
                                        {
                                            item.withPdf &&
                                            <Ionicons name="document-text" size={30} />
                                        }
                                        {
                                            item.withAudio &&
                                            <Ionicons name="headset" size={30} />
                                        }
                                    </View>
                                </View>
                            </View>
                        </TouchableOpacity>
                    </View>
                )
            }
        </View>
    )
}

export default BookItems