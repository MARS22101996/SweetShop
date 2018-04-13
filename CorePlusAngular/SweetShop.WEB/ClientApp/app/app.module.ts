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
import * as FusionCharts from 'fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';
import * as FintTheme from 'fusioncharts/themes/fusioncharts.theme.fint';
import { FusionChartsModule } from 'angular4-fusioncharts';
import { MatCardModule, MatToolbarModule, MatToolbar, MatButtonModule, MatButton, MatMenuModule } from '@angular/material';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LoginFormComponent } from "./account/login-form/login-form.component";
import { RegistrationFormComponent } from "./account/registration-form/registration-form.component";
import { UserService } from "./shared/services/user.service";
import { SpinnerComponent } from "./spinner/spinner.component";
import { ConfigService } from "./shared/utils/config.service";
import { CommonModule }       from '@angular/common';
import { HttpModule } from "@angular/http";
import { AuthGuard } from "./auth.guard";
import { FacebookLoginComponent } from "./account/facebook-login/facebook-login.component";
import { FavouritesComponent } from "./favourites/favourites-component";
import { BasketService } from "./shared/services/basket.service";
import { BasketComponent } from "./basket/basket.component";
import { ShipmentComponent } from "./shipment/shipment.component";
import { ContactComponent } from "./contact/contact.component";
import { FeedbackService } from "./shared/services/feedback.service";

FusionChartsModule.fcRoot(FusionCharts, Charts, FintTheme);

const appRoutes: Routes = [
    { path: 'home', component: HomePageComponent },
    { path: 'products', component: ProductListComponent,  canActivate: [AuthGuard]},
    { path: 'companies/create', component: CompanyCreateComponent},
    { path: 'companies', component: CompanyListComponent, canActivate: [AuthGuard] },
    { path: 'company/:id', component: CompanyDetailComponent },
    { path: 'product/:id', component: ProductDetailComponent },
    { path: 'company/edit/:id', component: CompanyEditComponent },
    { path: 'create', component: ProductCreateComponent },
    { path: 'edit/:id', component: ProductEditComponent },
    { path: 'statistic', component: StatisticProductsComponent, canActivate: [AuthGuard] },
    { path: 'login', component: LoginFormComponent },
    { path: 'facebookLogin', component : FacebookLoginComponent},
    { path: 'register', component: RegistrationFormComponent },
    { path: 'products/favourites', component: FavouritesComponent },
    { path: 'basket', component: BasketComponent },
    { path: 'shipment', component: ShipmentComponent },
    { path: 'contact', component: ContactComponent },   
    { path: '**', redirectTo: '/home' },
    { path: '', redirectTo: '/home', pathMatch: 'full' },
];

@NgModule({
    imports: [
        BrowserModule,
        FormsModule,
        ReactiveFormsModule,
        HttpClientModule,
        HttpModule,
        RouterModule.forRoot(appRoutes),
        MatCardModule,
        MatToolbarModule,
        MatButtonModule,
        MatMenuModule,
        FusionChartsModule,
        BrowserAnimationsModule,
        CommonModule,
        FormsModule],
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
        StatisticProductsComponent,
        LoginFormComponent,
        RegistrationFormComponent,
        SpinnerComponent,
        FacebookLoginComponent,
        FavouritesComponent,
        BasketComponent,
        ShipmentComponent,
        ContactComponent
    ],
    providers: [
        DataService,
        CompanyService,
        UserService,
        ConfigService,
        AuthGuard,
        BasketService,
        FeedbackService
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }