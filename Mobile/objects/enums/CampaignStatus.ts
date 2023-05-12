const CampaignStatus = {
    notStarted: 1,
    start: 2,
    end: 3,
    postpone: 4,
    toString: (n: number) => {
        if (n == CampaignStatus.notStarted) {
            return "Chưa diễn ra"
        }
        if (n == CampaignStatus.start) {
            return "Đang diễn ra"
        }
        if (n == CampaignStatus.end) {
            return "Đã kết thúc"
        }
        if (n == CampaignStatus.end) {
            return "Đã hủy"
        }
        return "";
    }
}
export default CampaignStatus;