import { Component } from '@angular/core';

import { UserService } from '../../shared/services/user.service';
import { Router } from '@angular/router';
 

@Component({
  selector: 'app-facebook-login',
  templateUrl: './facebook-login.component.html'
})
export class FacebookLoginComponent {

  private authWindow: Window;
  failed: boolean;
  error: string;
  errorDescription: string;
  isRequesting: boolean; 

  launchFbLogin() {
    this.authWindow = window.open('https://www.facebook.com/v2.11/dialog/oauth?&response_type=token&display=popup&client_id=476050682797666&display=popup&redirect_uri=http://localhost:54802/login&scope=email',null,'width=600,height=400');    
  }

  constructor(private userService: UserService, private router: Router) {
    if (window.addEventListener) {
      window.addEventListener("message", this.handleMessage.bind(this), false);
    } else {
       (<any>window).attachEvent("onmessage", this.handleMessage.bind(this));
    } 
  } 

  handleMessage(event: Event) {
    const message = event as MessageEvent;
    // Only trust messages from the below origin.
    if (message.origin !== "http://localhost:54802") {
      console.log("fail");
      return;
    }

    this.authWindow.close();

    const result = JSON.parse(message.data);
    console.log(result);
    
    if (!result.status)
    {
      this.failed = true;
      this.error = result.error;
      this.errorDescription = result.errorDescription;
    }
    else
    {
      this.failed = false;
      this.isRequesting = true;
     console.log("fail");
      this.userService.facebookLogin(result.accessToken)
        .finally(() => this.isRequesting = false)
        .subscribe(
        result => {
          if (result) {
            this.router.navigate(['/products']);
          }
        },
        error => {
          this.failed = true;
          this.error = error;
          console.log(error);
        });      
    }
  }
}
