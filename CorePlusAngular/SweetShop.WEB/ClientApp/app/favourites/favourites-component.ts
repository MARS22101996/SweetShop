import { Component, OnInit } from '@angular/core';

import { DataService } from "../products/data.service";
import { ProductView } from "../products/product-view";

@Component({
    templateUrl: './favourites-component.html',
    providers: [DataService]
})
export class FavouritesComponent implements OnInit {

    products: ProductView[];
    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.load();
    }

    load() {
        this.dataService.getFavoriteProducts().subscribe((data: ProductView[]) => this.products = data);
    }
}
