import { Component, Input } from '@angular/core';
import { Product } from './product';
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { CompanyService } from "../company/company.service";
import { Company } from "../company/company";


@Component({
    selector: "product-form",
    templateUrl: './product-form.component.html',
    styles: [`
     em { float: right; color: white; padding-left: 10px;}
    .error input, .error select { background-color: pink;}
  `]
})


export class ProductFormComponent {
    @Input() product: Product;
    companies: Company[];

    constructor(private dataService: CompanyService)
    { }

    ngOnInit() {
        this.load();

    }

    load() {
        this.dataService.getCompanies().subscribe((data: Company[]) => this.companies = data);
    }


}