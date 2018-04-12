import { OrderDetails } from "./order.detail";
import { OrderStatus } from "../enums/order-status";

export class Order {
    constructor(
        public orderId: number,
        public customerId: number,
        public paymentState: OrderStatus,
        public date?: string,
        public shippedDate?: string,
        public sum?: number,
        public deliveryPrice?: number,
        public orderDetails?: OrderDetails[]) { }
}