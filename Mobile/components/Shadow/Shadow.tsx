import React from 'react'
import { View, ViewProps } from 'react-native'

function Shadow(props: ViewProps) {
    return (
        <View
            {...props}
            style={Object.assign({}, props.style, {
                shadowColor: "#000",
                shadowOffset: {
                    width: 0,
                    height: 12,
                },
                shadowOpacity: 0.58,
                shadowRadius: 16.00,
                elevation: 24
            })}>
            {props.children}
        </View>
    )
}

export default Shadow