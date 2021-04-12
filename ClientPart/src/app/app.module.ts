import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule} from "@angular/common/http";
import {HomeComponent} from './components/home/home.component';
import {AUTH_API_URL} from "./app-injection-tokens";
import {environment} from "../environments/environment";
import {JwtModule} from "@auth0/angular-jwt";
import {ACCESS_TOKEN_KEY} from "./services/auth.service";
import {RouterModule} from "@angular/router";
import {LoginComponent} from './components/login/login.component';
import {OrderFormComponent} from './components/order-form/order-form.component';
import {OrderHistoryListComponent} from './components/order-history-list/order-history-list.component';
import {RegistrationComponent} from './components/registration/registration.component';
import {
  DxButtonModule,
  DxDataGridModule,
  DxDateBoxModule,
  DxFormModule,
  DxPopupModule,
  DxSelectBoxModule,
  DxTextBoxModule
} from "devextreme-angular";
import {ParkingListComponent} from './components/parking-list/parking-list.component';
import {OrderActiveListComponent} from './components/order-active-list/order-active-list.component';
import {OsmViewComponent} from './components/osm-view/osm-view.component';

export function tokenGetter() {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    OrderFormComponent,
    OrderHistoryListComponent,
    RegistrationComponent,
    ParkingListComponent,
    OrderActiveListComponent,
    OsmViewComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,

    RouterModule,

    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.tokenWhiteListedDomains
      }
    }),
    DxFormModule,
    DxButtonModule,
    DxDateBoxModule,
    DxSelectBoxModule,
    DxPopupModule,
    DxDataGridModule,
    DxTextBoxModule
  ],
  providers: [
    {
      provide: AUTH_API_URL,
      useValue: environment.authApi
    }],
  bootstrap: [AppComponent]
})
export class AppModule {
}
