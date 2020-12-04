import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {HomeComponent} from './components/home/home.component';
import {AuthGuard} from './guards/auth.guard';
import {LoginComponent} from './components/login/login.component';
import {RegistrationComponent} from './components/registration/registration.component';
import {ParkingListComponent} from './components/parking-list/parking-list.component';
import {OrderHistoryListComponent} from './components/order-history-list/order-history-list.component';
import {OrderActiveListComponent} from './components/order-active-list/order-active-list.component';

const routes: Routes = [
  {path: '', component: HomeComponent, canActivate: [AuthGuard]},
  {path: 'login', component: LoginComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'parking', component: ParkingListComponent, canActivate: [AuthGuard]},
  {path: 'history', component: OrderHistoryListComponent, canActivate: [AuthGuard]},
  {path: 'active', component: OrderActiveListComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {
}
