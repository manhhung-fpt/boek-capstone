import { useEffect, useRef, useState } from "react";
import { PagingProps } from "./Paging";
export default function usePagingComponent(props: PagingProps) {
    const [startPage, setStartPage] = useState(0);
    const [endPage, setEndPage] = useState(0);
    const [showStartDot, setShowStartDot] = useState(false);
    const [showEndDot, setShowEndDot] = useState(false);
    const range = (start: number, end: number) => Array.from(Array(end - start + 1).keys()).map(x => x + start);
    useEffect(() => {
        if (props.currentPage < 3) {
            setStartPage(1);
            if (props.maxPage > 5) {
                setEndPage(5);
            }
            else {
                setEndPage(props.maxPage);
            }
            setShowStartDot(false);
            if (props.maxPage > 5) {
                setShowEndDot(true);
            }
            else {
                setShowEndDot(false);
            }
            return;
        }
        if (props.currentPage + 3 < props.maxPage) {
            setStartPage(props.currentPage - 2);
            setEndPage(props.currentPage + 2);
            if (props.currentPage - 3 > 0) {
                setShowStartDot(true);
            }
            else {
                setShowStartDot(false);
            }
            setShowEndDot(true);
            return;
        }
        if (props.currentPage + 2 <= props.maxPage) {
            setEndPage(props.maxPage - (props.maxPage - props.currentPage) + 2);
            setStartPage(props.currentPage - 2);
            setShowStartDot(true);
            if (props.maxPage - props.currentPage > 2) {
                setShowEndDot(true);
            }
            else {
                setShowEndDot(false);
            }
            return;
        }
        if (props.currentPage == 3 || props.currentPage == 4) {
            setEndPage(props.maxPage);
            setStartPage(1);
            setShowStartDot(false);
            if (props.maxPage - props.currentPage > 2) {
                setShowEndDot(true);
            }
            else {
                setShowEndDot(false);
            }
            return;
        }
        else {
            setEndPage(props.maxPage);
            setStartPage(props.currentPage - 4);
            setShowStartDot(true);
            setShowEndDot(false);
        }
    }, [props.currentPage, props.maxPage]);
    return { startPage, endPage, showStartDot, showEndDot, range };
}