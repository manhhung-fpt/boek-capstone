export const BookFormat = {
    printBook: 1,
    pdf: 2,
    audio: 3,
    toString: (n: number) => {
        if (n == BookFormat.printBook) {
            return "Sách giấy"
        }
        if (n == BookFormat.pdf) {
            return "Pdf"
        }
        if (n == BookFormat.audio) {
            return "Audio"
        }
        return "";
    }
}