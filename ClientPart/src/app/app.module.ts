import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HttpClientModule} from '@angular/common/http';
import {HomeComponent} from './components/home/home.component';
import {AUTH_API_URL} from './app-injection-tokens';
import {environment} from '../environments/environment';
import {JwtModule} from '@auth0/angular-jwt';
import {ACCESS_TOKEN_KEY} from './services/auth.service';
import {RouterModule} from '@angular/router';
import {LoginComponent} from './components/login/login.component';
import {OrderFormComponent} from './components/order-form/order-form.component';
import {OrderHistoryListComponent} from './components/order-history-list/order-history-list.component';
import {RegistrationComponent} from './components/registration/registration.component';
import {LeafletModule} from '@asymmetrik/ngx-leaflet';
import {
  DxButtonModule,
  DxDataGridModule,
  DxDateBoxModule,
  DxFormModule, DxListModule, DxNumberBoxModule,
  DxPopupModule, DxScrollViewModule,
  DxSelectBoxModule,
  DxTextBoxModule, DxValidatorModule
} from 'devextreme-angular';
import {ParkingListComponent} from './components/parking-list/parking-list.component';
import {OrderActiveListComponent} from './components/order-active-list/order-active-list.component';
import {ParkingFormComponent} from './components/parking-list/parking-form/parking-form.component';
import {OsmViewComponent} from './components/osm-view/osm-view.component';
import { GeocodingComponent } from './components/osm-view/geocoding/geocoding.component';
import { MapPointFormComponent } from './components/osm-view/map-point-form/map-point-form.component';
import { ResultsListComponent } from './components/osm-view/results-list/results-list.component';
import {FormsModule} from '@angular/forms';
import { UserSettingsComponent } from './components/user-settings/user-settings.component';
import { UserBalanceComponent } from './components/user-balance/user-balance.component';
import {FontAwesomeModule} from "@fortawesome/angular-fontawesome";
import { CheckEmailComponent } from './components/check-email/check-email.component';
import { UserListComponent } from './components/user-list/user-list.component';
import {AnimateOnScrollModule} from "ng2-animate-on-scroll";

export function tokenGetter(): any {
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
    ParkingFormComponent,
    OsmViewComponent,
    GeocodingComponent,
    MapPointFormComponent,
    ResultsListComponent,
    UserSettingsComponent,
    UserBalanceComponent,
    CheckEmailComponent,
    UserListComponent,

  ],
  imports: [
    BrowserModule,
    AnimateOnScrollModule.forRoot(),
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
    DxTextBoxModule,
    LeafletModule,
    FormsModule,
    FontAwesomeModule,
    DxNumberBoxModule,
    DxListModule,
    DxValidatorModule,
    DxScrollViewModule
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
