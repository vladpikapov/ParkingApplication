import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {DxFormComponent} from 'devextreme-angular';
import {UserService} from '../../services/user.service';
import {SharedService} from '../../services/shared.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;

  @Output()
  checkCode = new EventEmitter();

  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }

  constructor(private as: AuthService, private us: UserService, private sharedService: SharedService) {
  }

  formData = {
    login: '',
    password: '',
  };

  ngOnInit(): void {
  }

  // tslint:disable-next-line:typedef
  login() {
    if (this.form && this.form.instance.validate().isValid) {
      this.as.login(this.formData.login, this.formData.password)
        .subscribe(_ => {
          if (!this.as.isConfirmEmail()) {
            this.us.getUser(this.as.currentUser.id).subscribe(res => {
              this.us.sendCodeToMail(res.email).subscribe(() => {
                this.checkCode.emit();
              });
            });
          }
          this.sharedService.userUpdate.emit();
          this.sharedService.parkingUpdate.emit();
        }, error => alert('Wrong login or password.'));
    }
  }

  // tslint:disable-next-line:typedef
  logout() {
    this.as.logout();
  }

}
