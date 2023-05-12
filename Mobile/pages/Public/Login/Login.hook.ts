import AsyncStorage from "@react-native-async-storage/async-storage";
import { GoogleSignin, User } from "@react-native-google-signin/google-signin";
import { useState } from "react";
import Toast from "react-native-toast-message";
import appxios, { setAuthorizationBearer } from "../../../components/AxiosInterceptor";
import useAppContext from "../../../context/Context";
import useAuth from "../../../libs/hook/useAuth";
import useRouter from "../../../libs/hook/useRouter";
import { Role } from "../../../objects/enums/Role";
import { BaseResponseModel } from "../../../objects/responses/BaseResponseModel";
import { LoginViewModel } from "../../../objects/viewmodels/Users/LoginViewModel";
import endPont from "../../../utils/endPoints";
import StorageKey from "../../../utils/storageKey";
import auth from '@react-native-firebase/auth';
import { StackScreenProps } from "@react-navigation/stack";
import { ParamListBase } from "@react-navigation/native";
import { SessionStorage } from "../../../utils/SessionStogare";

export default function useLoginPage(props: StackScreenProps<ParamListBase>) {
    const { navigate, replace } = useRouter();
    const [loading, setLoading] = useState(false);
    const { authenticated, setAuthorize } = useAuth();
    const { setUser, user } = useAppContext();

    const logout = async (navigate?: boolean) => {
        if (await GoogleSignin.getCurrentUser()) {
            await GoogleSignin.signOut();
        }
        if (auth().currentUser) {
            await auth().signOut();
        }
        setAuthorize(false);
        setAuthorizationBearer();
        setUser(undefined);
        await AsyncStorage.removeItem(StorageKey.user);
        props.navigation.reset({
            index: 0,
            routes: [],
        });
        if (navigate) {
            props.navigation.replace("Campaigns", { reset: Math.random() });
        }
    }
    const onLogin = async () => {
        const currentUser = await googleLogin();
        setLoading(true);
        if (!currentUser) {
            setLoading(false);
            return;
        }
        const idToken = await currentUser.getIdToken();
        console.log(currentUser.photoURL);
        console.log(idToken);
        const request = {
            idToken: idToken
        };
        const loginResponse = await appxios.post<BaseResponseModel<LoginViewModel>>(endPont.public.login, request);
        if (loginResponse.status == 200) {
            if (loginResponse.data.data.role != Role.customer && loginResponse.data.data.role != Role.staff) {
                Toast.show({
                    type: "error",
                    text1: "Đăng nhập thất bại",
                    text2: "Tài khoản của bạn không được phép đăng nhập"
                });
                await logout();
                setLoading(false);
                return;
            }
            await loginSuccess(loginResponse.data.data);
            const user = loginResponse.data.data;
            if (
                !user.address ||
                !user.name ||
                !user.phone) {
                replace("AskPersonalInformation");
            }
            else {
                props.navigation.reset({
                    index: 0,
                    routes: [],
                });
                if (user.role == Role.staff) {
                    props.navigation.replace("StaffCampaigns");
                }
                else {
                    props.navigation.replace("Campaigns", { reset: Math.random() });
                }
            }
            console.log(loginResponse.data);
            return loginResponse.data.data;
        }
        else {
            SessionStorage.setItem(StorageKey.createCustomerRequest, JSON.stringify({ idToken: idToken }));
            replace("AskPersonalInformation");
        }
        setLoading(false);
    }
    const loginSuccess = async (user: LoginViewModel) => {
        setLoading(false);
        setUser(user);
        await AsyncStorage.setItem(StorageKey.user, JSON.stringify(user));
        setAuthorizationBearer(user.accessToken);
        setAuthorize([user.role.toString()]);
    }

    const googleLogin = async () => {
        if (auth().currentUser) {
            return auth().currentUser;
        }
        try {
            await GoogleSignin.hasPlayServices({ showPlayServicesUpdateDialog: true });
            if (await GoogleSignin.isSignedIn()) {
                await GoogleSignin.signOut();
            }
            //await auth().signOut();
            let user = {} as User;
            user = await GoogleSignin.signIn();
            const googleCredential = auth.GoogleAuthProvider.credential(user.idToken);
            const credential = await auth().signInWithCredential(googleCredential);
            return credential.user;
        } catch (error) {
            if (error) {
                Toast.show({
                    type: "error",
                    text1: "Đăng nhập thất bại",
                    text2: "Quá trình đăng nhập được hủy"
                });
            }
            setLoading(false);
        }
    }

    return {
        ui: {
            loading
        },
        event: {
            onLogin
        }
    }
}