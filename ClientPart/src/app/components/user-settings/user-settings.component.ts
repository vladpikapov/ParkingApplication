import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../services/auth.service';
import {UserService} from '../../services/user.service';
import {Account} from '../../Models/Account';
import {DxFormComponent} from 'devextreme-angular';
import {SharedService} from '../../services/shared.service';
import notify from 'devextreme/ui/notify';

@Component({
  selector: 'app-user-settings',
  templateUrl: './user-settings.component.html',
  styleUrls: ['./user-settings.component.scss']
})
export class UserSettingsComponent implements OnInit {

  userInfo: Account;
  loginPopup = false;
  passwordPopup = false;
  mailPopup = false;
  carNumberPopup = false;
  codeSend = false;
  passwordForm = {
    password: ''
  };

  carNumberForm = {
    carNumber: ''
  };

  loginForm = {
    login: ''
  };

  mailForm = {
    mail: ''
  };

  mailCodeForm = {
    code: ''
  };

  constructor(private us: UserService, private as: AuthService, private sharedService: SharedService) {
  }

  ngOnInit(): void {
    this.us.getUser(this.as.currentUser.id).toPromise().then(res => {
      this.userInfo = res;
    });
  }


  onSave(logForm: DxFormComponent): void {
    if (logForm.instance.validate().isValid) {
      this.userInfo.login = this.loginForm.login;
      this.us.updateUserData(this.userInfo, 1).subscribe(res => {
        if (res) {
          notify('Success update login!', 'success');
          this.as.currentUser.login = this.loginForm.login;
          this.sharedService.userUpdate.emit();
          this.loginPopup = !this.loginPopup;
        } else {
          notify('Login already used!', 'error');
        }
      });
    }
  }

  openPasswordPopup(): void {
    this.passwordPopup = !this.passwordPopup;
  }

  openMailPopup(): void {
    this.mailPopup = !this.mailPopup;
  }

  openLoginPopup(): void {
    this.loginPopup = !this.loginPopup;
  }

  openCarNumberPopup(): void {
    this.carNumberPopup = !this.carNumberPopup;
  }

  updatePassword(formComponent: DxFormComponent): void {
    if (formComponent.instance.validate().isValid) {
      this.userInfo.password = this.passwordForm.password;
      this.us.updateUserData(this.userInfo, 2).subscribe(res => {
          if (res) {
            notify('Success update password', 'success');
            this.sharedService.userUpdate.emit();
            formComponent.instance.resetValues();
            this.passwordPopup = !this.passwordPopup;
          }
        }, error => notify(error, 'error')
      );
    }
    // this.us.updateUserData()
  }

  passwordComparison = () => {
    return this.passwordForm.password;
  }

  sendMailCode(form: DxFormComponent): void {
    if (form.instance.validate().isValid) {
      this.us.sendCodeToMail(this.mailForm.mail).subscribe(res => {
        if (res) {
          notify('Code send', 'default');
          this.codeSend = true;
          this.userInfo.email = this.mailForm.mail;
          this.mailPopup = !this.mailPopup;
        } else {
          notify('Mail already used', 'error');
        }
      });
    }
  }

  updateCarNumber(carNumberForm: DxFormComponent): void {
    if (carNumberForm.instance.validate().isValid) {
      this.userInfo.carNumber = this.carNumberForm.carNumber;
      this.us.updateUserData(this.userInfo, 4).subscribe(res => {
        if (res) {
          notify('Success update car number', 'success');
          this.sharedService.userUpdate.emit();
          this.carNumberPopup = !this.carNumberPopup;
        } else {
          notify('Car number already used', 'error');
        }
      });
    }
  }
}
