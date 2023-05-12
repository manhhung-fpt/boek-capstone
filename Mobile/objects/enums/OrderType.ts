export enum OrderType {
    Shipping = 1,
    PickUp = 2
}

export namespace OrderType {
    export function getLabel(value: OrderType): string {
        switch (value) {
            case OrderType.Shipping:
                return "Giao hàng;Shipping";
            case OrderType.PickUp:
                return "Nhận tại quầy;Pick-up";
            default:
                throw new Error(`Invalid value for OrderType: ${value}`);
        }
    }
}
