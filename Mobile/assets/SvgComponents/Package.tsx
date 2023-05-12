import React from 'react'
import Svg, { Path, SvgProps } from 'react-native-svg'

function Package(props: SvgProps) {
    return (
        <Svg height={48} width={48} {...props}>
            <Path d="m19 20.6 5-2.5 5 2.5V9H19ZM14 34v-4h10v4Zm-5 8q-1.2 0-2.1-.9Q6 40.2 6 39V9q0-1.2.9-2.1Q7.8 6 9 6h30q1.2 0 2.1.9.9.9.9 2.1v30q0 1.2-.9 2.1-.9.9-2.1.9ZM9 9v30V9Zm0 30h30V9h-7v16.45l-8-4-8 4V9H9v30Z" />
        </Svg>
    )
}

export default Package