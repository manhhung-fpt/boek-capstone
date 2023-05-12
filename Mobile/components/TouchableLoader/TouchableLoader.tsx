import { PropsWithChildren } from 'react';
import { TouchableOpacity, ActivityIndicator, GestureResponderEvent } from 'react-native'
import useTouchableLoaderComponent from './TouchableLoader.hook';
export interface TouchableLoaderProps extends PropsWithChildren {
    onLoadingPress: (e: GestureResponderEvent) => Promise<void>;
}
function TouchableLoader(props: TouchableLoaderProps) {
    const { loading, doWork } = useTouchableLoaderComponent(props);
    return (
        <>
            {
                loading ? <ActivityIndicator /> : <TouchableOpacity onPress={async e => await doWork(e)} >{props.children}</TouchableOpacity>
            }
        </>
    )
}

export default TouchableLoader