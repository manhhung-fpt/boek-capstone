import { BookViewModel } from "../Books/BookViewModel";

export interface BookProductItemViewModel {
    id?: number;
    parentBookProductId: string;
    bookId?: number;
    format?: number;
    displayIndex?: number;
    withPdf?: boolean;
    pdfExtraPrice?: number;
    displayPdfIndex?: number;
    withAudio: boolean;
    audioExtraPrice?: number;
    displayAudioIndex?: number;
    book?: BookViewModel;
}