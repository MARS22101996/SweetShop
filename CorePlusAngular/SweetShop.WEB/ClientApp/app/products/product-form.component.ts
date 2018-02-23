import { Component, Input } from '@angular/core';
import { Product } from './product';
import { FormGroup, FormControl, Validators } from "@angular/forms";


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
}