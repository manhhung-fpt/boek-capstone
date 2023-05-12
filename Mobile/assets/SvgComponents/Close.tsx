import React from 'react'
import Svg, { Path, SvgProps } from 'react-native-svg'
interface Props extends SvgProps {

}
function Close(props: Props) {
    return (
        <Svg
            height={48}
            viewBox="0 96 960 960"
            width={48}
            {...props}>
            <Path fill={props.color} d="m249 849-42-42 231-231-231-231 42-42 231 231 231-231 42 42-231 231 231 231-42 42-231-231-231 231Z" />
        </Svg>
    )
}

export default Close