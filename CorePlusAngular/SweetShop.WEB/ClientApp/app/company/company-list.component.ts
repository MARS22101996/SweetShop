import { Component, OnInit } from '@angular/core';
import { CompanyService } from './company.service';
import { Company } from './company';

@Component({
    templateUrl: './company-list.component.html',
    providers: [CompanyService]
})
export class CompanyListComponent implements OnInit {

    companies: Company[];
    constructor(private dataService: CompanyService) { }

    ngOnInit() {
        this.load();
    }

    load() {
        this.dataService.getCompanies().subscribe((data: Company[]) => this.companies = data);
    }

    delete(id: number) {
        this.dataService.deleteCompany(id).subscribe(data => this.load());
    }
}
