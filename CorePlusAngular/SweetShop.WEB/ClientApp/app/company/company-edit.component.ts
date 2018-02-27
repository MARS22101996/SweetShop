import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CompanyService } from "./company.service";
import { Company } from "./company";

@Component({
    templateUrl: './company-edit.component.html'
})
export class CompanyEditComponent implements OnInit {

    id: number;
    company: Company;
    loaded: boolean = false;

    constructor(private dataService: CompanyService, private router: Router, activeRoute: ActivatedRoute) {
        this.id = Number.parseInt(activeRoute.snapshot.params["id"]);
    }

    ngOnInit() {
        if (this.id)
            this.dataService.getCompany(this.id)
                .subscribe((data: Company) => {
                    this.company = data;
                    if (this.company != null) this.loaded = true;
                });
    }

    save() {
        this.dataService.updateCompany(this.company).subscribe(data => this.router.navigateByUrl("/companies"));
    }
}