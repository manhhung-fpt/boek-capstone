import AsyncStorage from '@react-native-async-storage/async-storage';
import useAsyncEffect from "use-async-effect";
import appxios, { setAuthorizationBearer } from '../components/AxiosInterceptor';
import useAuth from '../libs/hook/useAuth';
import { LoginViewModel } from '../objects/viewmodels/Users/LoginViewModel';
import StorageKey from '../utils/storageKey';
import useAppContext from "./Context";
import auth from '@react-native-firebase/auth';
import { BaseResponseModel } from '../objects/responses/BaseResponseModel';
import EndPont from '../utils/endPoints';
import * as Notifications from 'expo-notifications';
import useDebounce from '../libs/hook/useDebounce';
import useIsFirstRender from '../libs/hook/useIsFirstRender';
import { GoogleSignin } from '@react-native-google-signin/google-signin';
import { CampaignInCart } from '../objects/models/CampaignInCart';
import { HubConnectionBuilder } from "@microsoft/signalr";

export default function useInit() {
  const { setUser, cart, setCart, setTotalProductQuantity } = useAppContext();
  const { setAuthorize, initLoading, setInitLoading } = useAuth();
  const debounceCart = useDebounce(cart, 900);
  const isFirstRender = useIsFirstRender();
  useAsyncEffect(async () => {
    if (!isFirstRender) {
      await AsyncStorage.setItem(StorageKey.cart, JSON.stringify(cart));
    }
  }, [debounceCart]);

  useAsyncEffect(async () => {
    //AsyncStorage.removeItem(StorageKey.cart);
    const jsonString = await AsyncStorage.getItem(StorageKey.cart);
    //console.log(jsonString);

    let storeCart: CampaignInCart[] = [];

    if (jsonString != null) {
      storeCart = JSON.parse(jsonString) as CampaignInCart[];
      if (cart.length == 0) {
        setCart(storeCart);
      }
    }

    if (storeCart.length > 0) {
      let totalQuantity = 0;
      storeCart.map(c => c.issuersInCart.map(i => i.productsInCart.map(p =>
        totalQuantity += p.quantity
      )));
      setTotalProductQuantity(totalQuantity);
    }

    const userJsonString = await AsyncStorage.getItem(StorageKey.user);

    //await auth().signOut();
    //await GoogleSignin.signOut();

    if (auth().currentUser) {
      let user: LoginViewModel | undefined;
      if (userJsonString) {
        //console.log(userJsonString);
        user = JSON.parse(userJsonString);
      }
      else {
        const request = {
          idToken: await auth().currentUser?.getIdToken()
        };
        const loginResponse = await appxios.post<BaseResponseModel<LoginViewModel>>(EndPont.public.login, request);
        if (loginResponse.status == 200) {
          user = loginResponse.data.data;
        }
        else {
          await auth().signOut();
          await GoogleSignin.signOut();
        }
      }
      if (user) {
        setUser(user);
        setAuthorize([user.role.toString()]);
        setAuthorizationBearer(user.accessToken);
        console.log(appxios.defaults.headers.common['Authorization']);
      }
    }
    // console.log(appxios.defaults.baseURL);

    // const signalr = new HubConnectionBuilder()
    //   .withUrl(`${appxios.defaults.baseURL as string}`)
    //   .build();

    if (initLoading) {
      setInitLoading(false);
    }
    const onPressNotificationListener = Notifications.addNotificationResponseReceivedListener(response => {
      console.log(response.notification.request.content.data);
    });
    return onPressNotificationListener;
  }, []);
}