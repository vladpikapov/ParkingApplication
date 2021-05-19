import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {AuthGuard} from './guards/auth.guard';
import {LoginComponent} from './components/login/login.component';
import {RegistrationComponent} from './components/registration/registration.component';
import {ParkingListComponent} from './components/parking-list/parking-list.component';
import {OrderHistoryListComponent} from './components/order-history-list/order-history-list.component';
import {OrderActiveListComponent} from './components/order-active-list/order-active-list.component';
import {OsmViewComponent} from './components/osm-view/osm-view.component';
import {RoleGuard} from './guards/role.guard';
import {UserSettingsComponent} from './components/user-settings/user-settings.component';
import {UserBalanceComponent} from './components/user-balance/user-balance.component';
import {ParkingFormComponent} from './components/parking-list/parking-form/parking-form.component';
import {CheckEmailComponent} from './components/check-email/check-email.component';
import {UserListComponent} from './components/user-list/user-list.component';

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'parking', component: ParkingListComponent, canActivate: [AuthGuard]},
  {path: 'history', component: OrderHistoryListComponent, canActivate: [AuthGuard]},
  {path: 'active', component: OrderActiveListComponent, canActivate: [AuthGuard]},
  {path: 'parking-edit', component: ParkingFormComponent, canActivate: [AuthGuard, RoleGuard]},
  {path: 'user-settings', component: UserSettingsComponent, canActivate: [AuthGuard]},
  {path: 'user-balance', component: UserBalanceComponent, canActivate: [AuthGuard]},
  {path: 'confirm-email', component: CheckEmailComponent},
  {path: 'user-list', component: UserListComponent, canActivate: [AuthGuard, RoleGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
