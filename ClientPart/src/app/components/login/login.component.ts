import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }

  constructor(private as: AuthService) {
  }

  ngOnInit(): void {
  }

  // tslint:disable-next-line:typedef
  login(email: string, password: string) {
    this.as.login(email, password)
      .subscribe(res => {
      }, error => alert('Wrong login or password.'));
  }

  // tslint:disable-next-line:typedef
  logout() {
    this.as.logout();
  }

}
