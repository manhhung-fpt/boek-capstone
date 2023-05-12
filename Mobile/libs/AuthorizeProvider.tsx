import { createContext, Dispatch, PropsWithChildren, SetStateAction, useState } from "react"
class AuthorizeStore {
    initLoading: boolean = true;
    setInitLoading: Dispatch<SetStateAction<boolean>> = () => { };

    authenticated: boolean = false;
    roles: string[] = [];
    setAuthorize : (scheme: string[] | boolean) => void = () => {};
}
const useProvider: () => AuthorizeStore = () => {
    const [initLoading, setInitLoading] = useState<boolean>(true);
    const [authenticated, setAuthenticated] = useState<boolean>(false);
    const [roles, setRoles] = useState<string[]>([]);
    const setAuthorize = (scheme: string[] | boolean) => {
        if (!(typeof scheme == "boolean")) {
            setAuthenticated(true);
            setRoles(scheme);
        }
        else
        {
            setAuthenticated(scheme);
            setRoles([]);
        }
    }
    return {
        initLoading,
        setInitLoading,

        authenticated,
        roles,
        setAuthorize
    };
}
export const AuthorizeContext = createContext<AuthorizeStore>(new AuthorizeStore());
export default function AuthorizeProvider({children} : PropsWithChildren)
{
    return (
        <AuthorizeContext.Provider value={useProvider()} >
            {children}
        </AuthorizeContext.Provider>
    );
}