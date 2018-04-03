import { OrderDetails } from "./order.detail";

export class Order {
    constructor(
        public date?: string,
        public shippedDate?: string,
        public sum?: number,
        public deliveryPrice?: number,
        public orderDetails?: OrderDetails[]) { }
}