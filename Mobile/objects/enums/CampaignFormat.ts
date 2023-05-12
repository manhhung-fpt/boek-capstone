export const CampaignFormat = {
    offline: 1,
    online: 2,
    toString: (n: number) => {
        if (n == CampaignFormat.offline) {
            return "Trực tiếp"
        }
        if (n == CampaignFormat.online) {
            return "Trực tuyến"
        }
        return "";
    }
}