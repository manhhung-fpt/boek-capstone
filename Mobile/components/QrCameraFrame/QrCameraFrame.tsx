import { BarCodeScannedCallback, BarCodeScanner } from 'expo-barcode-scanner'
import React from 'react'
import { View, Image, Text, Dimensions } from 'react-native'
import logo from "../../assets/logo.png";
import { paletteGray } from '../../utils/color';
import useQrCameraFrameComponent from './QrCameraFrame.hook';
export interface QrCameraFrameProps {
    onBarCodeScanned: BarCodeScannedCallback;
    onCameraPermissionError?: Function;
    scanQr: boolean;
}
function QrCameraFrame(props: QrCameraFrameProps) {
    const { cameraPermission } = useQrCameraFrameComponent(props);
    return (
        <View style={{
            overflow: "hidden",
            width: "100%",
            height: "100%",
            backgroundColor: paletteGray,
            borderRadius: 30
        }}>
            {
                cameraPermission &&
                props.scanQr &&
                <BarCodeScanner
                    onBarCodeScanned={props.onBarCodeScanned}
                    style={{
                        //height : "100%"
                        height: "200%",
                        marginTop : -200
                    }} />
            }
            <View style={{
                display: props.scanQr && cameraPermission ? "none" : "flex",
                height: "100%",
                width: "100%",
                alignItems: "center",
                justifyContent: "center"
            }}>
                <Image source={logo} style={{ width: "90%" }} resizeMode={'contain'} />
            </View>
        </View>
    )
}

export default QrCameraFrame