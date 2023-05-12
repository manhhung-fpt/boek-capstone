import React, { PropsWithChildren, useState } from 'react'
import { Alert, Modal, Pressable, StyleProp, View, ViewStyle } from 'react-native'
interface LayoutModalProps extends PropsWithChildren {
    visible: boolean;
    onClose: () => void;
    closeOverlay?: boolean;
    overlayStyle?: StyleProp<ViewStyle>;
}
function LayoutModal(props: LayoutModalProps) {
    return (
        <Modal
            animationType="slide"
            transparent={true}
            visible={props.visible}
            onRequestClose={props.onClose}>
            <Pressable style={Object.assign({}, props.overlayStyle, { width: "100%", height: "100%" })} onPress={() => props.closeOverlay && props.onClose()}   >
                {props.children}
            </Pressable>
        </Modal>
    )
}

export default LayoutModal