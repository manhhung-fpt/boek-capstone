export enum OrderPayment {
    Unpaid = 1,
    ZaloPay = 2,
    Cash = 3
}

export namespace OrderPayment {
    export function getLabel(value: OrderPayment): string {
        switch (value) {
            case OrderPayment.Unpaid:
                return "Chưa thanh toán;Unpaid";
            case OrderPayment.ZaloPay:
                return "Thanh toán ZaloPay;ZaloPay";
            case OrderPayment.Cash:
                return "Thanh toán tiền mặt;Cash";
            default:
                throw new Error(`Invalid value for OrderPayment: ${value}`);
        }
    }
}
