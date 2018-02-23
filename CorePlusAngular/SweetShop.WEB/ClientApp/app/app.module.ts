import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { Routes, RouterModule } from '@angular/router';
import { ProductListComponent } from './products/product-list.component';
import { ProductDetailComponent } from './products/product-detail.component';
import { ProductCreateComponent } from './products/product-create.component';
import { ProductEditComponent } from './products/product-edit.component';
import { DataService } from './products/data.service';
import { NotFoundComponent } from './error/not-found.component';
import { ProductFormComponent } from './products/product-form.component';
import { NavBarComponent } from "./navbar/navbar.component";


const appRoutes: Routes = [
    { path: 'products', component: ProductListComponent },
    { path: 'product/:id', component: ProductDetailComponent },
    { path: 'create', component: ProductCreateComponent },
    { path: 'edit/:id', component: ProductEditComponent },
    { path: '**', redirectTo: '/' },
    { path: '', redirectTo: '/products', pathMatch: 'full' },
];

@NgModule({
    imports: [BrowserModule, FormsModule, HttpClientModule, RouterModule.forRoot(appRoutes)],
	declarations: [AppComponent, ProductListComponent, ProductDetailComponent, NotFoundComponent, ProductEditComponent, ProductCreateComponent, ProductFormComponent, NavBarComponent],
    providers: [DataService],
    bootstrap: [AppComponent]
})
export class AppModule { }