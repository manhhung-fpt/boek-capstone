export interface BasicBookViewModel {
    id?: number;
    code: string;
    genreId?: number;
    publisherId?: number;
    issuerId?: string;
    isbn10?: string;
    isbn13?: string;
    name: string;
    translator: string
    imageUrl: string;
    coverPrice: number;
    description: string;
    language: string;
    size: string;
    releasedYear: number;
    page: number;
    isSeries?: boolean;
    pdfExtraPrice?: number;
    pdfTrialUrl?: string;
    audioExtraPrice?: number;
    audioTrialUrl?: string;
    status: number;
    statusName: string;
    fullPdfAndAudio: boolean;
    onlyPdf: boolean;
    onlyAudio: boolean;
}