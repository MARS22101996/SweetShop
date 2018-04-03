import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data.service';

import { ProductView } from "./product-view";
import { Router } from "@angular/router";
import { Product } from "./product";
import { ProductLike } from "./product-like";
import { BasketService } from "../shared/services/basket.service";
import { OrderDetails } from "../shared/models/order.detail";

@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html',
    styleUrls: ['./product-list.component.css'],
    providers: [DataService]
})
export class ProductListComponent implements OnInit {

    @Input() companyId: number;
    products: ProductView[];
    sortBy: string;
    productForLike: Product;
    status: ProductLike;
    constructor(
        private dataService: DataService,
        private router: Router,
        private basketService: BasketService) { }

    ngOnInit() {
        this.chooseLoader()
    }

    chooseLoader() {
        if (this.companyId !== undefined) {
            this.loadByCompany();
        }
        else {
            this.load();
        }
    }


    load() {
        this.dataService.getProducts().subscribe((data: ProductView[]) => this.products = data);
    }

    loadByCompany() {
        this.dataService.getProductsByCompany(this.companyId).subscribe((data: ProductView[]) => this.products = data);
    }

    buy(product: ProductView) {
        this.basketService.buy(product).subscribe(data => this.load());
    }

    delete(id: number) {
        this.dataService.deleteProduct(id).subscribe(data => this.load());
    }

    sortByName() {
        this.products.sort(sortByNameExpression)
        this.sortBy = 'name'
    }

    sortByCompany() {
        this.products.sort(sortByCompanyExpression)
        this.sortBy = 'company'
    }

    sortByPrice() {
        this.products.sort(sortByPriceExpression)
        this.sortBy = 'price'
    }

    addLike(p: ProductView) {
        this.dataService.likeByUser(p)
            .subscribe(data => this.chooseLoader());
    }
}


function sortByNameExpression(s1: ProductView, s2: ProductView) {
    if (s1.name > s2.name) {
        return 1;
    }

    if (s1.name < s2.name) {
        return -1;
    }

    return 0;
}

function sortByCompanyExpression(s1: ProductView, s2: ProductView) {
    if (s1.company > s2.company) {
        return 1;
    }

    if (s1.company > s2.company) {
        return -1;
    }

    return 0;
}

function sortByPriceExpression(s1: ProductView, s2: ProductView) {
    if (s1.price > s2.price) {
        return 1;
    }

    if (s1.price > s2.price) {
        return -1;
    }

    return 0;
}
