import React, { useContext, useEffect, useState } from 'react';
import PDFView from 'react-native-view-pdf';
import useRouter from '../../../libs/hook/useRouter';
import { View, Text } from 'react-native';
import AudioPlayer from '../../../components/AudioPlayer/AudioPlayer';
import Shadow from '../../../components/Shadow/Shadow';
import { Button } from '@rneui/base';
import useAppContext from '../../../context/Context';
function Index() {
  const { navigate } = useRouter();
  const [scanQr, setScanQr] = useState(false);
  const [modalVisible, setModalVisible] = useState(false);
  const { resetCart } = useAppContext();
  return (
    <View style={{
      backgroundColor: "white",
      height: "100%"
    }}>
      {/* <AudioPlayer audioUri='http://10.10.2.106:6969/Starting%20Line-SLwboaO8POs.mp3' /> */}
      <Shadow style={{
        backgroundColor: "white",
        elevation: 12
      }}>
        <View style={{
          backgroundColor: "white"
        }}>
          <Text>hello</Text>
        </View>
      </Shadow>
      <Button onPress={() => resetCart()}>Reset card</Button>

      {/* <View style={{
        padding: 10
      }}>
       "https://file-examples.com/storage/fe0b804ac5640668798b8d0/2017/11/file_example_MP3_700KB.mp3"
        <AudioProgressBar duration={99} onSeek={() => { }} />
      </View> */}

      {/* <PDFView
        fadeInDuration={250}
        style={{ width: "100%", height: "100%", }}
        resource="https://www.africau.edu/images/default/sample.pdf"
        resourceType="url"
        onLoad={() => console.log(`PDF rendered from `)}
        onError={() => console.log('Cannot render PDF')} /> */}

      {/* <TouchableOpacity onPress={() => navigate("AskOrganizations")}>
        <Text>Ask Organization</Text>
      </TouchableOpacity>
      <TouchableOpacity onPress={() => navigate("AskPersonalInformation")}>
        <Text>Ask PersonalInformation</Text>
      </TouchableOpacity>
      <TouchableSafeAreaViewOpacity onPress={() => navigate("AskGenres")}>
        <Text>AskGenres</Text>
      </TouchableSafeAreaViewOpacity>
      <TouchableOpacity onPress={() => setModalVisible(true)}>
        <Text>Modal</Text>
      </TouchableOpacity>
      <LayoutModal
        visible={modalVisible}
        onClose={() => { setModalVisible(false) }}>
        <TouchableOpacity onPress={() => { setModalVisible(false) }}>
          <Text>dit me may</Text>
        </TouchableOpacity>
      </LayoutModal> */}

      {/* <Button onPress={async () => scanQr ? setScanQr(false) : setScanQr(true)} >Scan</Button>
      <View style={{ width: "100%", height: "100%", alignItems: "center", justifyContent: "center" }}>
        <View style={{ width: 350, height: 350 }}>
          <QrCameraFrame onBarCodeScanned={(e) => { alert(e.data); setScanQr(false) }} scanQr={scanQr} onCameraPermissionError={() => setScanQr(false)} />
        </View>
      </View> */}
    </View>
  )
}
export default Index;