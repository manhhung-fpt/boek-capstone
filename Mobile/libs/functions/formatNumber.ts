export default function formatNumber(num: number | undefined) {
    if (num) {
        return num.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.")    
    }
    return "";
}