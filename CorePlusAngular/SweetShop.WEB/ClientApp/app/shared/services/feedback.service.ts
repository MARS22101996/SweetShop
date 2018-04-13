import { Injectable } from "@angular/core";
import { BaseService } from "./base.service";
import { Http } from "@angular/http";
import { Feedback } from "../models/feedback";

@Injectable()
export class FeedbackService extends BaseService {

    private url = "/api/feedback";

    constructor(private http: Http) {
        super();
    }

    create(feedback: Feedback) {
        return this.http.post(this.url, feedback)
            .map(res => true)
            .catch(this.handleError);;
    }
}
