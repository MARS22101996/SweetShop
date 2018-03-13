import { Component, OnDestroy, OnInit } from '@angular/core'
import { UserService } from "../shared/services/user.service";
import { Router } from "@angular/router";
import { Subscription } from "rxjs/Subscription";

@Component({
   selector: 'nav-bar',
   templateUrl: './navbar.component.html',
   styles: [`
       li > a.active { color: orange !important; }`
   ]
})

export class NavBarComponent implements OnInit, OnDestroy {
    
    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }
    ngOnInit(): void {
        this.subscription = this.userService.authNavStatus$.subscribe(status => this.status = status);
    }
    status: boolean;
    subscription: Subscription;
    constructor(private userService: UserService, private router: Router) {
    }

    logout() {
        this.userService.logout();
    }
}
