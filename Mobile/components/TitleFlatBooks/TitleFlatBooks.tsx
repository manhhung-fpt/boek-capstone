import { useRoute } from '@react-navigation/native';
import { Button } from '@rneui/base';
import React, { useReducer } from 'react'
import { FlatList, } from 'react-native'
import { Text } from "@react-native-material/core";
import useRouter from '../../libs/hook/useRouter';
import { MobileBookProductViewModel } from '../../objects/viewmodels/BookProduct/Mobile/MobileBookProductViewModel';
import { BookViewModel } from '../../objects/viewmodels/Books/BookViewModel';
import BookCard from '../BookCard/BookCard';
interface TitleFlatBooksProps {
    data: MobileBookProductViewModel[];
    title: string;
}
function TitleFlatBooks(props: TitleFlatBooksProps) {
    const { navigate } = useRouter();
    return (
        <>
            <Text variant='h6' style={{ fontWeight: "600", marginBottom: 10, marginTop: 10 }}>{props.title}</Text>
            <FlatList
                horizontal
                data={props.data}
                renderItem={e =>
                    <BookCard book={e.item} />
                }
            />
        </>
    )
}

export default TitleFlatBooks