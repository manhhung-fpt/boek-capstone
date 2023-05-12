import React from 'react'
import Svg, { Path, SvgProps } from 'react-native-svg'

function Filber(props: SvgProps) {
    return (
        <Svg height={48} width={48} {...props}>
            <Path d="M23.9 24.1ZM24 38q-5.85 0-9.925-4.075Q10 29.85 10 24q0-5.85 4.075-9.925Q18.15 10 24 10q5.85 0 9.925 4.075Q38 18.15 38 24q0 5.85-4.075 9.925Q29.85 38 24 38Zm0-3q4.6 0 7.8-3.2T35 24q0-4.6-3.2-7.8T24 13q-4.6 0-7.8 3.2T13 24q0 4.6 3.2 7.8T24 35Z" />
        </Svg>
    )
}

export default Filber