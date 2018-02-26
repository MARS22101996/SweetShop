import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CompanyService } from './company.service';
import { Company } from './company';

@Component({
    templateUrl: './company-create.component.html'
})
export class CompanyCreateComponent {

    company: Company = new Company();
    constructor(private dataService: CompanyService, private router: Router) { }
    save() {
        this.dataService.createCompany(this.company).subscribe(data => this.router.navigateByUrl("/companies"));
    }
}