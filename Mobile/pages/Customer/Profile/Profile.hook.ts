import { Toast } from "react-native-toast-message/lib/src/Toast";
import auth from '@react-native-firebase/auth';
import { ProfileProps } from "./Profile";
import AsyncStorage from "@react-native-async-storage/async-storage";
import { GoogleSignin, User } from "@react-native-google-signin/google-signin";
import useRouter from "../../../libs/hook/useRouter";
import useAppContext from "../../../context/Context";
import useAuth from "../../../libs/hook/useAuth";
import appxios, { setAuthorizationBearer } from "../../../components/AxiosInterceptor";
import { BaseResponseModel } from "../../../objects/responses/BaseResponseModel";
import { LoginViewModel } from "../../../objects/viewmodels/Users/LoginViewModel";
import EndPont from "../../../utils/endPoints";
import StorageKey from "../../../utils/storageKey";
import { Role } from "../../../objects/enums/Role";
import { useEffect, useState } from "react";
import { SessionStorage } from "../../../utils/SessionStogare";
import endPont from "../../../utils/endPoints";
import useAsyncEffect from "use-async-effect";
import { CustomerViewModel } from "../../../objects/viewmodels/Users/customers/CustomerViewModel";
import { LevelViewModel } from "../../../objects/viewmodels/Levels/LevelViewModel";
import { BaseResponsePagingModel } from "../../../objects/responses/BaseResponsePagingModel";

export default function useProfilePage(props: ProfileProps) {
    const { setUser, user } = useAppContext();
    const { authenticated, setAuthorize } = useAuth();

    const [loading, setLoading] = useState(false);
    const [levelModalShowed, setLevelModalShowed] = useState(false);

    const [customer, setCustomer] = useState<CustomerViewModel>();
    const [levels, setLevels] = useState<LevelViewModel[]>([]);


    const logout = async (navigate?: boolean) => {
        if (await GoogleSignin.getCurrentUser()) {
            await GoogleSignin.signOut();
        }
        if (auth().currentUser) {
            await auth().signOut();
        }
        if (!auth().currentUser) {
            setAuthorize(false);
            setAuthorizationBearer();
            setUser(undefined);
            await AsyncStorage.removeItem(StorageKey.user);
            props.navigation.reset({
                index: 0,
                routes: [],
            });
            if (navigate) {
                props.navigation.jumpTo("Campaigns", { reset: Math.random() });
            }
        }
    }
    const randomColor = () => {
        var color = Math.floor(Math.random() * Math.pow(256, 3)).toString(16);
        while (color.length < 6) {
            color = "0" + color;
        }
        return "#" + color;
    }

    useAsyncEffect(async () => {
        if (user && user.role == Role.customer) {
            appxios.get<CustomerViewModel>(endPont.users.me).then(response => {
                setCustomer(response.data);
            });
            const query = new URLSearchParams();
            query.append("Status", "true");
            query.append("WithCustomers", "false");
            query.append("Size", "1000");
            query.append("Page", "1");
            appxios.get<BaseResponsePagingModel<LevelViewModel>>(`${endPont.levels.index}?${query.toString()}`).then(response => {
                setLevels(response.data.data);

            });
        }
    }, []);

    return {
        ui: {
            levelModalShowed,
            setLevelModalShowed,
            randomColor
        },
        event:
        {
            logout,
        },
        data: {
            customer,
            levels
        }

    };
    // const [enableBiometric, toogleEnableBiometric, setenableBiometric] = useBoolean(false);
    // const enableBiometricSuccess = () => {
    //     Toast.show({
    //         type: "success",
    //         text1: "Thông báo",
    //         text2: "Kích hoạt khóa sinh trắc học thành công"
    //     });
    // }
    // const enableBiometricFail = () => {
    //     Toast.show({
    //         type: "error",
    //         text1: "Thông báo",
    //         text2: "Kích hoạt khóa sinh trắc học thất bại"
    //     });
    //     setenableBiometric(false);
    // }
    // useAsyncEffect(() => {

    // }, []);
    // useAsyncEffect(async () => {
    //     if (!enableBiometric) {
    //         return;
    //     }
    //     if (!(await TouchID.isSupported())) {
    //         alert("Thiết bị của bạn không hỗ trợ khóa sinh trắc học");
    //         setenableBiometric(false);
    //         return;
    //     }
    //     const optionalConfigObject: AuthenticateConfig = {
    //         title: 'Yêu cầu xác thực', // Android
    //         imageColor: '#e00606', // Android
    //         imageErrorColor: '#ff0000', // Android
    //         sensorDescription: 'Chạm cảm biến vân tay', // Android
    //         sensorErrorDescription: 'Lỗi', // Android
    //         cancelText: 'Hủy', // Android
    //         fallbackLabel: 'Hiện mã pin', // iOS (if empty, then label is hidden)
    //         unifiedErrors: false, // use unified error messages (default false)
    //         passcodeFallback: false, // iOS - allows the device to fall back to using the passcode, if faceid/touch is not available. this does not mean that if touchid/faceid fails the first few times it will revert to passcode, rather that if the former are not enrolled, then it will use the passcode.
    //     };
    //     try {
    //         const result: boolean = await TouchID.authenticate('Để chắc chắn bạn là chủ sở hữu của thiết bị này', optionalConfigObject);
    //         if (result) {
    //             enableBiometricSuccess();
    //         }
    //         else {
    //             enableBiometricFail();
    //         }
    //     } catch (error) {
    //         enableBiometricFail();
    //     }

    // }, [enableBiometric]);
    // return {enableBiometric, toogleEnableBiometric};

}