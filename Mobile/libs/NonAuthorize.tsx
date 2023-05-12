import React, { PropsWithChildren, useContext, useEffect} from 'react';
import { forbiddenRedirect } from '../utils/Redirect';
import { AuthorizeContext } from './AuthorizeProvider';
import useRouter from './hook/useRouter';
interface Props extends PropsWithChildren {
    forbiddenRedirect?: string;
}
function NonAuthorize(props: Props) {
    const { authenticated, initLoading } = useContext(AuthorizeContext);
    const { replace } = useRouter();
    useEffect(() => {
        if (!initLoading) {
            if (authenticated) {
                replace(props.forbiddenRedirect || forbiddenRedirect)
            }
        }
    }, [initLoading]);
    return (
        !initLoading ?
            authenticated ?
                null
                :
                <>{props.children}</>
            :
            null
    )
}

export default NonAuthorize