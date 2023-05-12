import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useRef, useState } from "react";
import { AudioPlayerRefProps } from "../../../components/AudioPlayer/AudioPlayer";
import { BookProductItemViewModel } from "../../../objects/viewmodels/BookProductItems/BookProductItemViewModel";

export default function useBookItemDetailPage(props: StackScreenProps<ParamListBase>) {
    const audioPlayerRef = useRef<AudioPlayerRefProps>(null);
    const [bookItem, setBookItem] = useState<BookProductItemViewModel>();

    const [descriptionExpanded, setDescriptionExpanded] = useState(false);
    const [informationExpanded, setInformationExpanded] = useState(false);
    const [trialAudioVisible, setTrialAudioVisible] = useState(false);

    useEffect(() => {
        const params = props.route.params as { data: BookProductItemViewModel };
        setBookItem(params.data);
        props.navigation.setOptions({
            title: params.data.book?.name
        });
    }, []);

    const showPdfOrAudio = () => {
        if (bookItem?.withPdf || bookItem?.withAudio) {
            return true;
        }
        return false;
    }

    const onTrialAudioClose = async () => {
        await audioPlayerRef.current?.pause();
        setTrialAudioVisible(false);
    }

    return {
        ref: {
            audioPlayerRef
        },
        data: {
            bookItem
        },
        event: {
            onTrialAudioClose
        },
        ui: {
            showPdfOrAudio,
            trialAudioVisible,
            setTrialAudioVisible,
            descriptionExpanded,
            setDescriptionExpanded,
            informationExpanded,
            setInformationExpanded
        }
    }
}