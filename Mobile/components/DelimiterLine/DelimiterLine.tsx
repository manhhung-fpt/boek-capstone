import React from 'react'
import { View } from 'react-native'
import { paletteGray } from '../../utils/color'

function DelimiterLine() {
    return (
        <View style={{ borderWidth: 0.8, borderColor: paletteGray, marginBottom: 15 }}></View>

    )
}

export default DelimiterLine