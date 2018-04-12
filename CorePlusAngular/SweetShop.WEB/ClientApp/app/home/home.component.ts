import { Component } from '@angular/core'

@Component({
   selector: 'home-page',
   templateUrl: './home.component.html',
})

export class HomePageComponent {
    imageUrl: string;

     ngOnInit(){
        this.imageUrl = "/img/intro.jpg";
    }
}
