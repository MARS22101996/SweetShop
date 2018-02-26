import { Component, Input } from '@angular/core';
import { Company } from './company';
import { FormGroup, FormControl, Validators } from "@angular/forms";


@Component({
    selector: "company-form",
    templateUrl: './company-form.component.html',
    styles: [`
     em { float: right; color: white; padding-left: 10px;}
    .error input, .error select { background-color: pink;}
  `]
})


export class CompanyFormComponent {
    @Input() company: Company;
}