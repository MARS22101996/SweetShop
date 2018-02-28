import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CompanyService } from './company.service';
import { Company } from "./company";

@Component({
	templateUrl: './company-detail.component.html',
	providers: [CompanyService]
})
export class CompanyDetailComponent implements OnInit {

	id: number;
	company: Company;
	loaded: boolean = false;

	constructor(private dataService: CompanyService, activeRoute: ActivatedRoute) {
		this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
	}

	ngOnInit() {
		if (this.id)
			this.dataService.getCompany(this.id)
				.subscribe((data: Company) => { this.company = data; this.loaded = true; });
	}
}