import { Component } from '@angular/core'

@Component({
   selector: 'nav-bar',
   templateUrl: './navbar.component.html',
   styles: [`
       li > a.active { color: orange !important; }`
   ]
})

export class NavBarComponent {}
