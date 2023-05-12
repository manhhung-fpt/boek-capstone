export enum BookProductStatus {
    Pending = 1,
    Rejected = 2,
    Sale = 3,
    NotSale = 4,
    NotSaleDueEndDate = 5,
    NotSaleDueCancelledCampaign = 6,
    NotSaleDuePostponedCampaign = 7,
    OutOfStock = 8,
    Unreleased = 9,
}

export namespace BookProductStatus {
    export function toDisplayString(status: BookProductStatus): string {
        switch (status) {
            case BookProductStatus.Pending:
                return "Chờ duyệt";
            case BookProductStatus.Rejected:
                return "Từ chối";
            case BookProductStatus.Sale:
                return "Đang bán";
            case BookProductStatus.NotSale:
                return "Ngừng bán";
            case BookProductStatus.NotSaleDueEndDate:
                return "Ngừng bán";
            case BookProductStatus.NotSaleDueCancelledCampaign:
                return "Ngừng bán";
            case BookProductStatus.NotSaleDuePostponedCampaign:
                return "Ngừng bán";
            case BookProductStatus.OutOfStock:
                return "Hết hàng";
            case BookProductStatus.Unreleased:
                return "Ngừng phát hành";
            default:
                throw new Error(`Unsupported bookProductStatus value: ${status}`);
        }
    }
}
