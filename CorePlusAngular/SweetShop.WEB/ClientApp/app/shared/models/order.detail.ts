import { Product } from "../../products/product";

export class OrderDetails {
    orderDetailsId?: number;
    price: number;
    quantity: number;
    discount: number;
    orderId?: number;
    productId: number;
    product: Product;
}
