import { Component, OnInit } from '@angular/core';
import { DataService } from "../products/data.service";
import { StatisticByProducts } from "./statistic-by-products";

@Component({
    templateUrl: './statistic-products.component.html',
    providers: [DataService]
})
export class StatisticProductsComponent implements OnInit {

    statistic: StatisticByProducts[];
    constructor(private dataService: DataService) { }

    ngOnInit() {
        this.load();
    }

    load() {
        this.dataService.getStatisticByProducts().subscribe((data: StatisticByProducts[]) => this.statistic = data);
    }
}