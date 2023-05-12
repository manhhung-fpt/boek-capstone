import React, { Fragment, PropsWithChildren, useContext } from 'react'
import useAuth from './hook/useAuth';
interface Props extends PropsWithChildren<{}> {
    roles?: string[];
}
export default function AuthorizeView(props: Props) {
    const { authenticated, roles } = useAuth();
    const isInRole = () => {
        if (!props.roles) {
            return true;
        }
        for (let i = 0; i < roles.length; i++) {
            const element = roles[i];
            if (props.roles.includes(element)) {
                return true;
            }
        }
        return false;
    }
    return (
        authenticated ?
            isInRole() ?
                <Fragment>
                    {props.children}
                </Fragment> :
                null
            :
            null
    )
}