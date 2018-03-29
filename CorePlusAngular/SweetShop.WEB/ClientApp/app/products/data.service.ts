import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from './product';
import { UserService } from "../shared/services/user.service";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { BaseService } from "../shared/services/base.service";
import { ProductView } from "./product-view";

@Injectable()
export class DataService extends BaseService {

	private url = "/api/products";
	private headers: Headers;

	constructor(private http: HttpClient, private httpClient: Http, private user: UserService) {
		super();
	}


	getProducts() {
		if (this.user.isLoggedIn()) {
			let headers = this.setUpHeaders();
			return this.httpClient.get(this.url, { headers })
				.map(response => response.json())
				.catch(this.handleError);;
		}
		return null;
	}

	getFavoriteProducts() {
		if (this.user.isLoggedIn()) {
			let headers = this.setUpHeaders();
			return this.httpClient.get(this.url + '/favourites', { headers })
				.map(response => response.json())
				.catch(this.handleError);;
		}
		return null;
	}

	getProductsByCompany(id: number) {
		return this.http.get(this.url + '/company/' + id);
	}

	getStatisticByProducts() {
		return this.http.get(this.url + '/statistic');
	}

	getStatisticByCompany() {
		return this.http.get(this.url + '/statistic/company');
	}


	getProduct(id: number) {
		return this.http.get(this.url + '/' + id);
	}

	getProductRawData(id: number) {
		return this.http.get(this.url + '/raw/' + id);
	}

	createProduct(product: Product) {
		return this.http.post(this.url, product, { observe: 'response' });
	}

	likeByUser(productView: ProductView) {
		if (this.user.isLoggedIn()) {
			let product = this.addLike(productView);
			let options = this.setUpOptions();
			return this.httpClient.put(this.url + '/' + product.id, product, options)
				.map(res => true)
				.catch(this.handleError);;
		}
		return null;
	}

	updateProduct(product: Product) {
		if (this.user.isLoggedIn()) {
			let options = this.setUpOptions();
			return this.httpClient.put(this.url + '/' + product.id, product, options)
				.map(res => true)
				.catch(this.handleError);;
		}
		return null;
	}

	deleteProduct(id: number) {
		return this.http.delete(this.url + '/' + id);
	}

	deleteFromFavourites(id: number) {
		let options = this.setUpOptions();
		return this.httpClient.delete(this.url + '/favourites/' + id, options)
			.map(res => res.json())
			.catch(this.handleError);
	}

	checkLikeForUser(id: number) {
		if (this.user.isLoggedIn()) {
			let headers = this.setUpHeaders();
			return this.httpClient.get(this.url + '/checkLikes/' + id, { headers })
				.map(response => response.json())
				.catch(this.handleError);
		}
		return null;
	}

	setUpOptions() {
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');
		let authToken = localStorage.getItem('auth_token');
		headers.append('Authorization', `Bearer ${authToken}`);
		let options = new RequestOptions({ headers: headers });
		return options;
	}


	setUpHeaders() {
		let headers = new Headers();
		headers.append('Content-Type', 'application/json');
		let authToken = localStorage.getItem('auth_token');
		headers.append('Authorization', `Bearer ${authToken}`);
		return headers;
	}

	addLike(p: ProductView) {
		let productForLike = new Product();
		productForLike.id = p.id;
		productForLike.companyId = p.companyId;
		productForLike.description = p.description;
		productForLike.name = p.name;
		productForLike.price = p.price;
		productForLike.likes = p.likes + 1;
		return productForLike;
	}
}