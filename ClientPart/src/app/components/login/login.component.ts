import {Component, OnInit, ViewChild} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {DxFormComponent} from "devextreme-angular";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;


  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }

  constructor(private as: AuthService) {
  }

  formData = {
    mail: '',
    password: '',
  };

  ngOnInit(): void {
  }

  // tslint:disable-next-line:typedef
  login() {
    console.log(this.formData);
    if (this.form && this.form.instance.validate().isValid) {
      this.as.login(this.formData.mail, this.formData.password)
        .subscribe(res => {
        }, error => alert('Wrong login or password.'));
    }
  }

  // tslint:disable-next-line:typedef
  logout() {
    this.as.logout();
  }

}
