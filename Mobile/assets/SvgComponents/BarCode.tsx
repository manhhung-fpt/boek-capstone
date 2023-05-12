import React from 'react'
import Svg, { Path, SvgProps } from 'react-native-svg'

function BarCode(props: SvgProps) {
    return (
        <Svg height={48} width={48} {...props}>
            <Path d="M2 38V10h4.25v28Zm6 0V10h4v28Zm6 0V10h2v28Zm6 0V10h4v28Zm6 0V10h6v28Zm8 0V10h2v28Zm6 0V10h6v28Z" />
        </Svg>
    )
}

export default BarCode