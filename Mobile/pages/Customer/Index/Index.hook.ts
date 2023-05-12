import { createContext, Dispatch, SetStateAction, useState } from "react";
import useBoolean from "../../../libs/hook/useBoolean";


interface IndexContextData {
    searchValue: string;
    setSearchValue: Dispatch<SetStateAction<string>>;

    searchSubmitChange: boolean;
    toggleSearchSubmitChange: () => void;
}
export const IndexContext = createContext<IndexContextData>({} as IndexContextData);
export const indexContextProviderInit: () => IndexContextData = () => {
    const [searchValue, setSearchValue] = useState("");
    const [searchSubmitChange, toggleSearchSubmitChange] = useBoolean();
    return {
        searchValue,
        setSearchValue,

        searchSubmitChange,
        toggleSearchSubmitChange
    }
}
export default function useIndexPage() {
    return {};
}

