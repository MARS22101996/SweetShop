import { BaseService } from "./base.service";
import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { UserService } from "./user.service";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { OrderDetails } from "../models/order.detail";
import { ProductView } from "../../products/product-view";

@Injectable()
export class BasketService extends BaseService {

    private url = "/api/basket";
    private headers: Headers;

    constructor(private http: Http, private user: UserService) {
        super();
    }

    buyProduct(product: ProductView) {
        if (this.user.isLoggedIn()) {
            let orderDetail = this.createOrderDetail(product);
            let options = this.setUpHeaders();
            return this.http.post(this.url, orderDetail, options)
                .map(res => true)
                .catch(this.handleError);;
        }
        return null;
    }

    setUpHeaders() {
        let headers = new Headers();
        headers.append('Content-Type', 'application/json');
        let authToken = localStorage.getItem('auth_token');
        headers.append('Authorization', `Bearer ${authToken}`);
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