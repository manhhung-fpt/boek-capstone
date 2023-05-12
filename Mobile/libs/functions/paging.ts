export function getMaxPage(limit: number, total: number) {
    return Math.ceil(total / limit);
}