import {Component} from '@angular/core';
import {AuthService} from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }

  constructor(private as: AuthService) {
  }

  logout() {
    this.as.logout();
  }

  getRole() {
    return this.as.currentUser.Role;
  }

  getName() {
    return this.as.currentUser.Email;
  }

}
