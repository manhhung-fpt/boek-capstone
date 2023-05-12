import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useState } from "react";

export default function usePdfShowerPage(props: StackScreenProps<ParamListBase>) {
    const [loading, setLoading] = useState(false);

    const [url, setUrl] = useState("");
    useEffect(() => {
        setLoading(true);
        const params = props.route.params as { title: string, url: string };
        setUrl(params.url);
        props.navigation.setOptions({
            title: params.title
        });
        console.log(params.url);
    }, []);

    return {
        data: {
            url
        },
        ui: {
            loading,
            setLoading
        }
    }
}