import { useContext } from "react";
import { AuthorizeContext } from "../AuthorizeProvider";

export default function useAuth() {
    const { authenticated, roles, setAuthorize, initLoading, setInitLoading } = useContext(AuthorizeContext);
    const isInRole = (checkRoles: string[]) => {
        if (!checkRoles) {
            return true;
        }
        for (let i = 0; i < roles.length; i++) {
            const element = roles[i];
            if (checkRoles.includes(element)) {
                return true;
            }
        }
        return false;
    }
    return { authenticated, roles, setAuthorize, initLoading, setInitLoading, isInRole };
}