import { ParamListBase } from '@react-navigation/native';
import { StackScreenProps } from '@react-navigation/stack';
import { Button } from '@rneui/base';
import React, { useState } from 'react'
import { View } from 'react-native'
import PageLoader from '../../../components/PageLoader/PageLoader';
import QrCameraFrame from '../../../components/QrCameraFrame/QrCameraFrame';
import { paletteRed, primaryColor, primaryTint1 } from '../../../utils/color';
import { Text } from "@react-native-material/core"
import useCreateOrderScanQrPage from './CreateOrderScanQr.hook';

function CreateOrderScanQr(props: StackScreenProps<ParamListBase>) {
    const hook = useCreateOrderScanQrPage(props);

    return (
        <View>
            <PageLoader loading={hook.ui.loading} />
            <View style={{
                //borderWidth: 1,
                width: "100%",
                height: "60%",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <View style={{ width: 350, height: 350 }}>
                    <QrCameraFrame onBarCodeScanned={hook.event.onQrScaned} scanQr={hook.ui.scanQr} onCameraPermissionError={() => hook.ui.setScanQr(false)} />
                </View>
            </View>
            <View style={{
                height: "40%",
                alignItems: "center",
                justifyContent: "center",
                padding: 20
            }}>
                <Text style={{ textAlign: "center", marginBottom: "20%" }}>Hãy quét mã QR mà khách hàng đưa để xem chi tiết đơn hàng và tiến hành thanh toán</Text>
                <Button
                    onPress={() => hook.ui.setScanQr(!hook.ui.scanQr)}
                    buttonStyle={{
                        width: 180,
                        height: 60,
                        borderRadius: 8,
                        backgroundColor: hook.ui.scanQr ? paletteRed : primaryTint1
                    }}>{hook.ui.scanQr ? "Dừng quét mã" : "Quét mã"}</Button>
            </View>
        </View>
    )
}

export default CreateOrderScanQr