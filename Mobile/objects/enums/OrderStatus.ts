export enum OrderStatus {
    Pending = 1,
    Processing = 2,
    PickUpAvailable = 3,
    Shipping = 4,
    Shipped = 5,
    Received = 6,
    Cancelled = 7
}

export namespace OrderStatus {
    export function getLabel(value: OrderStatus): string {
        switch (value) {
            case OrderStatus.Pending:
                return "Đang chờ";
            case OrderStatus.Processing:
                return "Đang xử lý";
            case OrderStatus.PickUpAvailable:
                return "Chờ nhận hàng";
            case OrderStatus.Shipping:
                return "Đang vận chuyển";
            case OrderStatus.Shipped:
                return "Đã giao";
            case OrderStatus.Received:
                return "Đã nhận";
            case OrderStatus.Cancelled:
                return "Hủy";
            default:
                throw new Error(`Invalid value for OrderStatus: ${value}`);
        }
    }
}
