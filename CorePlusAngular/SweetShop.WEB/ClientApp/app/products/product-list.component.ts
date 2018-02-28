import { Component, OnInit, Input } from '@angular/core';
import { DataService } from './data.service';

import { ProductView } from "./product-view";

@Component({
    selector: 'product-list',
    templateUrl: './product-list.component.html',
    providers: [DataService]
})
export class ProductListComponent implements OnInit {

    @Input() companyId: number;
    products: ProductView[];
    sortBy: string;
    constructor(private dataService: DataService) { }

    ngOnInit() {
        if (this.companyId !== undefined) {
            console.log(this.companyId)
            this.loadByCompany();
        }
        else {
            this.load();
            console.log(this.companyId)
        }
    }

    load() {
        this.dataService.getProducts().subscribe((data: ProductView[]) => this.products = data);
    }

    loadByCompany() {
        this.dataService.getProductsByCompany(this.companyId).subscribe((data: ProductView[]) => this.products = data);
    }


    delete(id: number) {     
        this.dataService.deleteProduct(id).subscribe(data => this.load());
    }

    sortByName() {
        this.products.sort(sortByNameExpression)
        this.sortBy='name'
    }

    sortByCompany() {
        this.products.sort(sortByCompanyExpression)
        this.sortBy='company'
    }

    sortByPrice() {
        this.products.sort(sortByPriceExpression)
        this.sortBy='price'
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
