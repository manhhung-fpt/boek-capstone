import Toast from "react-native-toast-message";
import Provider from "./context/Provider";
import AuthorizeProvider from "./libs/AuthorizeProvider";
import React from "react";
import Routers from "./Routes";
import { GoogleSignin } from "@react-native-google-signin/google-signin";
import { StatusBar } from "react-native";
import { primaryColor } from "./utils/color";
import { MenuProvider } from "react-native-popup-menu";
import * as Notifications from 'expo-notifications';

Notifications.setNotificationHandler({
  handleNotification: async (notification) => {
    return {
      shouldShowAlert: true,
      shouldPlaySound: true,
      shouldSetBadge: false
    }
  },
});

GoogleSignin.configure({
  webClientId: '652368417331-i91jda7knc2ardd0pnkq0cr3vog446qf.apps.googleusercontent.com',
  iosClientId: "652368417331-1k2t1k8aks1g5483andfd5nja06th71d.apps.googleusercontent.com"
});

const App = () => {
  return (
    <>
      <StatusBar backgroundColor={primaryColor} barStyle="light-content" />
      <Provider>
        <AuthorizeProvider>
          <MenuProvider>
            <Routers />
          </MenuProvider>
        </AuthorizeProvider>
        <Toast />
      </Provider>
    </>
  );
}

export default App;