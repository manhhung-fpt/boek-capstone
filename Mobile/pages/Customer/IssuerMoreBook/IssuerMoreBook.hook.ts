import { ParamListBase } from "@react-navigation/native";
import { StackScreenProps } from "@react-navigation/stack";
import { useEffect, useRef, useState } from "react";
import { ScrollView } from "react-native";

export default function useIssuerMoreBookPage(props: StackScreenProps<ParamListBase>) {
    const [maxPage, setmaxPage] = useState(100);
    const [currentPage, setCurrentPage] = useState(1);
    const scrollViewRef = useRef<ScrollView>(null);
    const [index, setIndex] = useState(0);
    const onPageNavigation = (page: number) => {
      if (scrollViewRef.current) {
        scrollViewRef.current.scrollTo({y : 0});
        setCurrentPage(page);
      }
    }

    return {
        index,
        setIndex,
        maxPage,
        currentPage,
        onPageNavigation,
        scrollViewRef
    };
}