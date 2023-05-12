import { createContext, Dispatch, PropsWithChildren, SetStateAction, useState } from "react";

interface SearchPageContextData {
    searchValue: string;
    setSearchValue: Dispatch<SetStateAction<string>>;
    onSubmit: () => void;
    setOnSubmit: Dispatch<SetStateAction<() => void>>;
}
export const SearchPageContext = createContext<SearchPageContextData>({} as SearchPageContextData);


export function SearchPageContextProvider(props: PropsWithChildren) {
    const [searchValue, setSearchValue] = useState("");
    const [onSubmit, setOnSubmit] = useState<() => void>(() => { });
    return (
        <SearchPageContext.Provider value={{ searchValue, setSearchValue, onSubmit, setOnSubmit }}>
            {props.children}
        </SearchPageContext.Provider>
    );
}
