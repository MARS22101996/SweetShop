import { Component } from '@angular/core'
import { Feedback } from "../shared/models/feedback";
import { FeedbackService } from "../shared/services/feedback.service";
import { FormGroup, FormControl, Validators, NgForm } from "@angular/forms";
import { Router } from "@angular/router";

@Component({
   templateUrl: './contact.component.html',
   styleUrls: ['./contact.component.css']
})

export class ContactComponent {
    feedback: Feedback

    constructor(private dataService: FeedbackService, private router: Router) {
        this.feedback = new Feedback();
    }

    save(f: NgForm) {
        if (!f.invalid) {
            this.dataService.create(f.value).subscribe(data => { this.router.navigate(['/contact']); });
        }
    }
}
