import { ParamListBase } from '@react-navigation/native'
import { StackScreenProps } from '@react-navigation/stack'
import React from 'react'
import { View, Image } from 'react-native'
import { Text } from '@react-native-material/core'
import Swiper from 'react-native-swiper'
import GoogleLoginButton from '../../../components/GoogleLoginButton/GoogleLoginButton'
import Shadow from '../../../components/Shadow/Shadow'
import useLoginPage from './Login.hook'
import background from "../../../assets/background.jpg";
import logo from "../../../assets/logo.png";
import PageLoader from '../../../components/PageLoader/PageLoader'
import { paletteGray, paletteGrayTint7, paletteGrayTint8, paletteGrayTint9 } from '../../../utils/color'

function Login(props: StackScreenProps<ParamListBase>) {
    const hook = useLoginPage(props);
    return (
        <>
            <PageLoader loading={hook.ui.loading} />
            <View style={{
                width: "100%",
                height: "100%"
            }}>
                <Image source={background} style={{
                    position: "absolute",
                    width: "100%",
                    height: "100%",
                }} />
                <View style={{
                    position: "absolute",
                    width: "100%",
                    height: "100%",
                    backgroundColor: "rgba(0,0,0,0.5)"
                }} />
                <View style={{
                    width: "100%",
                    height: "90%",
                    alignItems: "center",
                    justifyContent: "center"
                }}>
                    <Shadow style={{
                        backgroundColor: "white",
                        height: "50%",
                        width: "80%",
                        borderRadius: 12,
                        padding: 20
                    }}>
                        <View style={{
                            height: "10%",
                            alignItems: "center"
                        }}>
                            <Text variant='h6'>Chào mừng bạn đến với Boek</Text>
                        </View>
                        <View style={{
                            //borderWidth : 1,
                            height: "70%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <Image source={logo} resizeMode="contain" style={{ width: "100%", height: "100%" }} />
                        </View>
                        <View style={{
                            height: "20%",
                            alignItems: "center",
                            justifyContent: "center"
                        }}>
                            <View style={{ width: 150 }}>
                                <GoogleLoginButton onPress={hook.event.onLogin} />
                            </View>
                        </View>
                    </Shadow>
                </View>
            </View>
        </>
    )
}

export default Login