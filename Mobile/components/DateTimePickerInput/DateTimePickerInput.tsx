import moment from 'moment';
import React, { useState } from 'react'
import { View, Text, Image, TouchableOpacity } from 'react-native'
import eventBlack from "../../assets/icons/event-black.png";
import closeBlack from "../../assets/icons/close-black.png";
import { paletteGray } from '../../utils/color';
import { dateFormat } from '../../utils/format';
import DateTimePickerModal from "react-native-modal-datetime-picker";
interface DateTimePickerInputProps {
    icon?: () => JSX.Element;
    hideReset?: boolean;
    format?: string;
    value?: Date;
    placeholder?: string;
    maximumDate?: Date;
    minimumDate?: Date;
    mode?: "date" | "time" | "datetime";
    onConfirm?: (date: Date) => void;
    onCancel?: () => void;
    onReset?: () => void;
}
function DateTimePickerInput(props: DateTimePickerInputProps) {
    const blankLabel = props.placeholder || "Chọn ngày"
    const [showModal, setShowModal] = useState(false);
    return (
        <>
            <View
                style={{
                    //borderWidth: 1,
                    flexDirection: "row",
                    width: "100%",
                    height: "100%"
                }}>
                <TouchableOpacity
                    onPress={() => setShowModal(true)}
                    style={{
                        //borderWidth: 1,
                        height: "100%",
                        flexDirection: "row",
                        width: props.hideReset == undefined ? "60%" : "80%",
                        alignItems: "center",
                        justifyContent: "flex-end"
                    }}>
                    <Text style={{
                        color: props.value == undefined ? paletteGray : "black",
                        textDecorationLine: "underline",
                        fontSize: 15,
                        marginRight: 7
                    }}>{props.value != undefined ?
                        props.format ?
                            moment(props.value).format(props.format) :
                            moment(props.value).format(dateFormat) :
                        blankLabel}
                    </Text>
                </TouchableOpacity>
                <TouchableOpacity
                    onPress={() => setShowModal(true)}
                    style={{
                        //borderWidth: 1,
                        width: props.hideReset == undefined ? "20%" : "20%",
                        alignItems: "center",
                        justifyContent: "center"
                    }}>
                    {
                        props.icon ?
                            <props.icon />
                            :
                            <Image source={eventBlack} style={{ width: 20, height: 20 }} resizeMode="contain" />
                    }
                </TouchableOpacity>
                {
                    props.hideReset == undefined &&
                    <TouchableOpacity
                        onPress={props.onReset}
                        style={{
                            //borderWidth: 1,
                            width: "20%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                        <Image source={closeBlack} style={{ height: 20, width: 20 }} resizeMode="contain" />
                    </TouchableOpacity>
                }
            </View>
            <DateTimePickerModal
                maximumDate={props.maximumDate}
                minimumDate={props.minimumDate}
                mode={props.mode}
                date={props.value}
                onConfirm={(date) => { setShowModal(false); props.onConfirm && props.onConfirm(date) }}
                onCancel={() => { setShowModal(false); props.onCancel && props.onCancel() }}
                isVisible={showModal} />
        </>
    )
}

export default DateTimePickerInput