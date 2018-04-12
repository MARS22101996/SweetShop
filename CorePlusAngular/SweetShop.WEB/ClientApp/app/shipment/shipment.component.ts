import { Component } from '@angular/core'

@Component({
    templateUrl: './shipment.component.html',
})

export class ShipmentComponent {
    inStock: boolean;

    ngOnInit(){
        this.inStock = true;
    }
}
