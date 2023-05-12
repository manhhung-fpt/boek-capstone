import { ParamListBase } from '@react-navigation/native';
import { StackScreenProps } from '@react-navigation/stack';
import { useState } from 'react';
import { View, Text, ScrollView, FlatList, TouchableOpacity } from 'react-native'
import BookCard from '../../../components/BookCard/BookCard';
import Paging from '../../../components/Paging/Paging';
import range from '../../../libs/functions/range';
import { primaryTint4 } from '../../../utils/color';
import { mockBooks } from '../../../utils/mock';
import useIssuerMoreBookPage from './IssuerMoreBook.hook';

function IssuerMoreBook(props: StackScreenProps<ParamListBase>) {
    const hook = useIssuerMoreBookPage(props);
    return (
        <>
            <View style={{ backgroundColor: "white" }}>
                <Text style={{ fontSize: 18, fontWeight: "600", margin: 10 }}>Sách giảm giá</Text>
                <FlatList
                    style={{ marginBottom: 10 }}
                    horizontal
                    data={range(10000, 10010)} renderItem={e =>
                        <TouchableOpacity
                            onPress={() => hook.setIndex(e.index)}
                            style={{
                                borderBottomColor: primaryTint4,
                                borderBottomWidth: e.index == hook.index ? 1 : 0,
                                paddingLeft: 20,
                                paddingRight: 20,
                                paddingBottom: 10,
                                paddingTop: 10
                            }}>
                            <Text style={{ fontSize: 16 }}>{e.item}</Text>
                        </TouchableOpacity>
                    } />
            </View>
            <ScrollView ref={hook.scrollViewRef} style={{ padding: 10, backgroundColor: "white" }}>
                <View style={{ flexDirection: "row", flexWrap: "wrap" }}>
                    {
                        mockBooks.map(item =>
                            <View key={Math.random()} style={{ width: "50%" }}>
                                <BookCard book={item} />
                            </View>
                        )
                    }
                </View>
                <View style={{ marginBottom: 20 }}>
                    <Paging onPageNavigation={hook.onPageNavigation} currentPage={hook.currentPage} maxPage={hook.maxPage} />
                </View>
            </ScrollView>
        </>
    )
}

export default IssuerMoreBook