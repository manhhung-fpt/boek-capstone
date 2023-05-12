export default function truncateString(str: string | undefined, wordCount: number): string {
    if (str) {
        const words = str.split(" ");
        if (words.length <= wordCount) {
            return str;
        } else {
            return words.slice(0, wordCount).join(" ") + "...";
        }
    }
    return "";
}