import { Button } from '@rneui/base';
import React, { useState } from 'react'
import { FlatList, TouchableOpacity } from 'react-native'
import { Text } from "@react-native-material/core";
import useRouter from '../../libs/hook/useRouter';
import { MobileBookProductViewModel } from '../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel';
import { BookViewModel } from '../../objects/viewmodels/Books/BookViewModel';
import { primaryTint4 } from '../../utils/color';
import BookCard from '../BookCard/BookCard';
interface TitleTabedFlatBooks {
    title?: string;
    data: { tabLabel: string, tabData: MobileBookProductViewModel[] }[];
}
function TitleTabedFlatBooks(props: TitleTabedFlatBooks) {
    const { navigate } = useRouter();
    const [index, setIndex] = useState(0);
    return (
        <>
            {
                props.title &&
                <Text variant='h6' style={{ fontWeight: "600", marginBottom: 10, marginTop: 10 }}>{props.title}</Text>
            }
            <FlatList
                style={{ marginBottom: 10 }}
                horizontal
                data={props.data} renderItem={e =>
                    <TouchableOpacity
                        onPress={() => setIndex(e.index)}
                        style={{
                            borderBottomColor: primaryTint4,
                            borderBottomWidth: e.index == index ? 1 : 0,
                            paddingLeft: 20,
                            paddingRight: 20,
                            paddingBottom: 10,
                            paddingTop: 10
                        }}>
                        <Text style={{ fontSize: 16 }}>{e.item.tabLabel}</Text>
                    </TouchableOpacity>
                } />
            <FlatList
                horizontal
                data={props.data[index].tabData}
                renderItem={e =>
                    <BookCard book={e.item} />
                }
            />
        </>
    )
}

export default TitleTabedFlatBooks