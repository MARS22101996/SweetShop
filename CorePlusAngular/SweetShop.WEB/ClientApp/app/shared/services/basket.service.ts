import { BaseService } from "./base.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { OrderDetails } from "../models/order.detail";
import { ProductView } from "../../products/product-view";
import { Order } from "../models/order";
import { OrderStatus } from "../enums/order-status";

@Injectable()
export class BasketService extends BaseService {

    private basketUrl = "/api/basket";
    private orderUrl = "/api/order";
    private headers: Headers;

    constructor(private http: Http, private user: UserService) {
        super();
    }

    buy(product: ProductView) {
        if (this.user.isLoggedIn()) {
            let orderDetail = this.createOrderDetail(product);
            var result = this.updateOrderDetail(orderDetail);
            return result;
        }
        return null;
    }

    updateOrderDetail(orderDetail: OrderDetails) {
        let options = this.setUpOptions();
        return this.http.post(this.basketUrl, orderDetail, options)
            .map(res => true)
            .catch(this.handleError);;
    }
    
    payOrder(order: Order) {
        order.paymentState = OrderStatus.payed;
        return this.updateOrder(order);
    }

    updateOrder(order: Order) {
        let options = this.setUpOptions();
        return this.http.post(this.orderUrl, order, options)
            .map(res => true)
            .catch(this.handleError);;
    }

    getBasket() {
        if (this.user.isLoggedIn()) {
            let headers = this.setUpHeaders()
            return this.http.get(this.basketUrl, { headers })
                .map(response => response.json())
                .catch(this.handleError);
        }
        return null;
    }

    delete(id: number) {
        return this.http.delete(this.basketUrl + '/' + id);
    }

    setUpHeaders() {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let authToken = localStorage.getItem('auth_token');
        headers.append('Authorization', `Bearer ${authToken}`);
        return headers;
    }

    setUpOptions() {
        let headers = this.setUpHeaders();
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    createOrderDetail(product: ProductView) {
        let orderDetail = new OrderDetails();
        orderDetail.discount = 0;
        orderDetail.price = product.price;
        orderDetail.quantity = product.quantity;
        orderDetail.productId = product.id;
        return orderDetail;
    }
}
