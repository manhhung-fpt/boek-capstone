import React, { PropsWithChildren } from 'react'
import { Context, useProvider } from './Context'

function Provider(props: PropsWithChildren<{}>) {
    return (
        <Context.Provider value={useProvider()}>
            {props.children}
        </Context.Provider>
    )
}

export default Provider