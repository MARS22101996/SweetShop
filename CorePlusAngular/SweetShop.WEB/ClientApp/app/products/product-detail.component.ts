import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DataService } from './data.service';
import { ProductView } from './product-view';

@Component({
	templateUrl: './product-detail.component.html',
	providers: [DataService]
})
export class ProductDetailComponent implements OnInit {

	id: number;
	product: ProductView;
	loaded: boolean = false;

	constructor(private dataService: DataService, activeRoute: ActivatedRoute) {
		this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
	}

	ngOnInit() {
		if (this.id)
			this.dataService.getProduct(this.id)
				.subscribe((data: ProductView) => { this.product = data; this.loaded = true; });
	}
}