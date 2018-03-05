import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { HomePageComponent } from "./home/home.component";
import { CompanyListComponent } from "./company/company-list.component";
import { CompanyService } from "./company/company.service";
import { CompanyCreateComponent } from "./company/company-create.component";
import { CompanyFormComponent } from "./company/company-form.component";
import { CompanyEditComponent } from "./company/company-edit.component";
import { CompanyDetailComponent } from "./company/company-detail.component";
import { StatisticProductsComponent } from "./statistic/statistic-products.component";
// import { GoogleChart } from 'angular2-google-chart/directives/angular2-google-chart.directive';
import * as FusionCharts from 'fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';
import * as FintTheme from 'fusioncharts/themes/fusioncharts.theme.fint';
import { FusionChartsModule } from 'angular4-fusioncharts';
import { MatCardModule, MatToolbarModule, MatToolbar, MatButtonModule, MatButton, MatMenuModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';  

FusionChartsModule.fcRoot(FusionCharts, Charts, FintTheme);

const appRoutes: Routes = [
    { path: 'home', component: HomePageComponent },
    { path: 'products', component: ProductListComponent },
    { path: 'companies/create', component: CompanyCreateComponent },
    { path: 'companies', component: CompanyListComponent },
    { path: 'company/:id', component: CompanyDetailComponent },
    { path: 'product/:id', component: ProductDetailComponent }, 
    { path: 'company/edit/:id', component: CompanyEditComponent },
    { path: 'create', component: ProductCreateComponent },
    { path: 'edit/:id', component: ProductEditComponent },
    { path: 'statistic', component: StatisticProductsComponent },
    { path: '**', redirectTo: '/products' },
    { path: '', redirectTo: '/products', pathMatch: 'full' },
];


@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        RouterModule.forRoot(appRoutes),
        MatCardModule,
        MatToolbarModule,
        MatButtonModule,
        MatMenuModule,
        FusionChartsModule,
        BrowserAnimationsModule],
    declarations: [
        AppComponent,
        ProductListComponent,
        ProductDetailComponent,
        NotFoundComponent,
        ProductEditComponent,
        ProductCreateComponent,
        ProductFormComponent,
        NavBarComponent,
        HomePageComponent,
        CompanyListComponent,
        CompanyCreateComponent,
        CompanyFormComponent,
        CompanyEditComponent,
        CompanyDetailComponent,
        StatisticProductsComponent
    ],
    providers: [DataService,
                CompanyService],
    bootstrap: [AppComponent]
})
export class AppModule { }