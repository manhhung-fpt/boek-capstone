import { useContext} from "react";
import { AuthorizeContext } from "../AuthorizeProvider";
import useEffectOnce from "./useEffectOnce";

export default function useAuthorizeInit() {
    const { setInitLoading, initLoading } = useContext(AuthorizeContext);
    useEffectOnce(() => {        
        if (initLoading) {
            setInitLoading(false);
        }
    });
}