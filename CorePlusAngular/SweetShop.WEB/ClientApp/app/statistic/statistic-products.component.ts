import { Component, OnInit } from '@angular/core';
import { DataService } from "../products/data.service";
import { StatisticByProducts } from "./statistic-by-products";

@Component({
    templateUrl: './statistic-products.component.html',
    providers: [DataService]
})
export class StatisticProductsComponent implements OnInit {
    statisticBy: string;
    id = 'AngularChart';
    width = '100%';
    height = '100%';
    type = 'pie3d';
    dataFormat = 'json';
    dataSource: any;
    chartData = {
        "caption": "Statistic for products by likes",
        "subcaption": "Last Year",
        "startingangle": "220",
        "showlabels": "1",
        "showlegend": "1",
        "enablemultislicing": "0",
        "slicingdistance": "25",
        "showpercentvalues": "1",
        "showpercentintooltip": "1",
        "bgColor": "EEEEEE,CCCCCC",
        "bgratio": "60,40",
        "bgAlpha": "20,80",
        "bgAngle": "180",
        "plottooltext": "Product: $label Total likes : $datavalue",
        "borderColor": "#666666",
        "borderThickness": "4",
        "borderAlpha": "50",
        "theme": "carbon",
    };
    statistic: StatisticByProducts[];
    constructor(private dataService: DataService) {
    }


    statisticByProducts() {
        this.statisticBy = "product"
        this.dataService.getStatisticByProducts().subscribe((data: StatisticByProducts[]) => {
            this.statistic = data;
            this.chartData.caption = "Statistic for products by likes"
            this.chartData.plottooltext = "Product: $label Total likes : $datavalue"
            this.dataSource = {
                "chart": this.chartData,
                "data": this.statistic
            };
        });
    }

    statisticByCompany() {
        this.statisticBy = "company"
        this.dataService.getStatisticByCompany().subscribe((data: StatisticByProducts[]) => {
            this.statistic = data;
            this.chartData.caption = "Statistic for companies by likes"
            this.chartData.plottooltext = "Company: $label Total likes : $datavalue"
            this.dataSource = {
                "chart": this.chartData,
                "data": this.statistic
            };
        });
    }

    ngOnInit() {
        this.load();
    }

    load() {
        this.dataService.getStatisticByProducts().subscribe((data: StatisticByProducts[]) => {
            this.statistic = data;
            this.dataSource = {
                "chart": this.chartData,
                "data": this.statistic
            };
        });
    }
}