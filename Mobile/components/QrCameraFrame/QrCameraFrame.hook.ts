import { BarCodeScanner } from "expo-barcode-scanner";
import { useState } from "react";
import { Toast } from "react-native-toast-message/lib/src/Toast";
import useAsyncEffect from "use-async-effect";
import { QrCameraFrameProps } from "./QrCameraFrame";

export default function useQrCameraFrameComponent(props: QrCameraFrameProps) {
    const [cameraPermission, setCameraPermission] = useState(false);
    useAsyncEffect(async () => {
        if (props.scanQr) {
            const { granted } = await BarCodeScanner.requestPermissionsAsync();
            if (!granted) {
                props.onCameraPermissionError && props.onCameraPermissionError();
                Toast.show(
                    {
                        type : "error",
                        text1 : "Lỗi",
                        text2 : "Quyền truy cập máy ảnh bị từ chối"
                    }
                )
            }
            setCameraPermission(granted); 
        }
    }, [props.scanQr]);
    return { cameraPermission };
}