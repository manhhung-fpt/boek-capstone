import { ParamListBase } from '@react-navigation/native'
import { StackScreenProps } from '@react-navigation/stack'
import React from 'react'
import { Toast } from 'react-native-toast-message/lib/src/Toast'
//import PDFView from 'react-native-view-pdf'
import PageLoader from '../../../components/PageLoader/PageLoader'
import useRouter from '../../../libs/hook/useRouter'
import usePdfShowerPage from './PdfShower.hook'
import Pdf from 'react-native-pdf';

function PdfShower(props: StackScreenProps<ParamListBase>) {
    const hook = usePdfShowerPage(props);
    const { goBack } = useRouter();
    return (
        <>
            <PageLoader loading={hook.ui.loading} />
            {
                hook.data.url &&
                <Pdf
                    style={{ width: "100%", height: "100%" }}
                    source={{uri : hook.data.url}}
                    onLoadProgress={() => hook.ui.setLoading(false)}
                    onError={() => { Toast.show({ text1: "Lỗi", text2: "Không thể truy cập pdf", type: "error" }); goBack() }} />
            }
        </>
    )
}

export default PdfShower