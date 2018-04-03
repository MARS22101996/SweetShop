import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BasketService } from "../shared/services/basket.service";
import { Order } from "../shared/models/order";
import { PercentPipe } from '@angular/common';
import { DatePipe } from '@angular/common';
import { OrderDetails } from "../shared/models/order.detail";

@Component({
    templateUrl: './basket.component.html'
})
export class BasketComponent implements OnInit {
    order: Order;
    orderDetails: OrderDetails[];
    constructor(private router: Router, private basketService: BasketService) { }

    ngOnInit() {
        this.load();
    }

    load() {
        this.basketService.getBasket().subscribe((data: Order) => {
            this.order = data;
            this.orderDetails = data.orderDetails
        });
    }

    delete(id: number) {
        this.basketService.delete(id).subscribe(data => {
            this.load();
        });
    }

    edit(orderDetails: OrderDetails) {
        this.basketService.updateOrderDetail(orderDetails).subscribe(data => this.load());
    }
}