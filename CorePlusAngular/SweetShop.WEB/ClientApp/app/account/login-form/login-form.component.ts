import { Subscription } from 'rxjs';
import { Component, OnInit,OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { UserService } from '../../shared/services/user.service';
import { Credentials } from "../../shared/models/credentials";

@Component({
  selector: 'app-login-form',
  templateUrl: './login-form.component.html',
  styleUrls: ['./login-form.component.css']
})

export class LoginFormComponent implements OnInit, OnDestroy {

  private subscription: Subscription;

  brandNew: boolean;
  errors: string;
  isRequesting: boolean;
  submitted: boolean = false;
  credentials: Credentials = { email: '', password: '' };

  constructor(private userService: UserService, private router: Router,private activatedRoute: ActivatedRoute) { }

    ngOnInit() {

    this.subscription = this.activatedRoute.queryParams.subscribe(
      (param: any) => {
         this.brandNew = param['brandNew'];   
         this.credentials.email = param['email'];         
      });      
  }

   ngOnDestroy() {
    // prevent memory leak by unsubscribing
    this.subscription.unsubscribe();
  }

   login({ value, valid }: { value: Credentials, valid: boolean }) {
     this.submitted = true;
     this.isRequesting = true;
     this.errors = '';
     if (valid) {
       this.userService.login(value.email, value.password)
         .finally(() => this.isRequesting = false)
         .subscribe(
         (result: any) => {
           if (result) {
             this.router.navigate(['/home']);
           }
         },
         (error: any) => this.errors = error);
     }
   }
}
