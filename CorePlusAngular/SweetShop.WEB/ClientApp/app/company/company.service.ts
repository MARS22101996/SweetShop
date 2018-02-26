import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Company } from './company';

@Injectable()
export class CompanyService {

	private url = "/api/companies";

	constructor(private http: HttpClient) {
	}

	getCompanies() {
		return this.http.get(this.url);
	}

	getCompany(id: number) {
		return this.http.get(this.url + '/' + id);
	}

	createCompany(company: Company) {
		return this.http.post(this.url, company, { observe: 'response' });
	}
	updateCompany(company: Company) {

		return this.http.put(this.url + '/' + company.id, company, { observe: 'response', responseType: 'text' });
	}
	deleteCompany(id: number) {
		return this.http.delete(this.url + '/' + id);
	}
}