import { Button } from '@rneui/base'
import React from 'react'
import { ButtonProps, GestureResponderEvent, View } from 'react-native'
import { primaryTint1 } from '../../utils/color';
interface ShowMoreButtonProps {
    onPress?: (event: GestureResponderEvent) => void;
    expanded?: boolean;
}
function ShowMoreButton(props: ShowMoreButtonProps) {
    return (
        <View style={{ width: "100%", alignItems: "center", justifyContent: "center", marginTop: 20 }}>
            <Button
                onPress={props.onPress}
                buttonStyle={{
                    width: 170,
                    alignItems: "center",
                    borderRadius: 8,
                    backgroundColor: primaryTint1
                }}>{props.expanded ? "Thu gọn" : "Xem thêm"}</Button>
        </View>
    )
}

export default ShowMoreButton