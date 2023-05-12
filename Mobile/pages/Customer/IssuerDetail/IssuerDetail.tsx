import { NavigationProp, ParamListBase, RouteProp } from '@react-navigation/native';
import { StackScreenProps } from '@react-navigation/stack';
import { Button } from '@rneui/base';
import React, { useEffect } from 'react'
import { ScrollView, View, Image, Text } from 'react-native';
import TitleFlatBooks from '../../../components/TitleFlatBooks/TitleFlatBooks';
import TitleTabedFlatBooks from '../../../components/TitleTabedFlatBooks/TitleTabedFlatBooks';
import avatar from "../../../assets/avatar.jpg";
import { mockBooks } from '../../../utils/mock';

function IssuerDetail(props: StackScreenProps<ParamListBase>) {
    useEffect(() => {
        props.navigation.setOptions({
            title: "Tên nhà phát hành"
        });
    }, []);
    return (
        <ScrollView style={{ backgroundColor: "white" }}>
            <View
                style={{
                    height: 160,
                    flexDirection: "row"
                }}>
                <View style={{
                    width: "40%",
                    alignItems: "center",
                    justifyContent: "center"
                }}>
                    <View style={{ borderRadius: 999, overflow: "hidden" }}>
                        <Image
                            source={avatar}
                            resizeMode="cover"
                            style={{
                                width: 130,
                                height: 130
                            }} />
                    </View>
                </View>
                <View style={{
                    width: "60%",
                    justifyContent: "center"
                }}>
                    <Text style={{ fontSize: 16, margin: 5 }}>Tên</Text>
                    <Text style={{ fontSize: 16, margin: 5 }}>SDT: </Text>
                    <Text style={{ fontSize: 16, margin: 5 }}>Địa chỉ:</Text>
                    <Text style={{ fontSize: 16, margin: 5 }}>Email:</Text>
                </View>
            </View>
            <View style={{ padding: 10 }}>
                <TitleFlatBooks data={mockBooks} title="Sách giảm giá" />
                <TitleTabedFlatBooks title="Thể loại 1" data={[
                    {
                        tabLabel: "Thể loại 1",
                        tabData: mockBooks
                    },
                    {
                        tabLabel: "Thể loại 1",
                        tabData: mockBooks
                    },
                    {
                        tabLabel: "Thể loại 1",
                        tabData: mockBooks
                    },
                ]} />
            </View>

        </ScrollView>
    )
}

export default IssuerDetail