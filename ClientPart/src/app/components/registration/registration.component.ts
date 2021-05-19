import {Component, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {DxFormComponent} from 'devextreme-angular';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';
import {UserService} from '../../services/user.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {

  @Output()
  checkMailCode = new EventEmitter<boolean>();

  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;

  formData = {
    login: '',
    email: '',
    carNumber: '',
    password: '',
    repeatPassword: ''
  };

  constructor(private router: Router, private as: AuthService, private userService: UserService) {

  }

  ngOnInit(): void {
  }

  registration(): void {
    if (this.form && this.form.instance.validate().isValid) {
      if (this.formData.password === this.formData.repeatPassword) {
        this.as.registration(this.formData.login, this.formData.password, this.formData.email, this.formData.carNumber).subscribe(res => {
          if (res === false) {
            alert('Such a user already exist!');
          } else {
            this.as.currentUser.login = this.formData.login;
            this.checkMailCode.emit(true);
          }
        });
      } else {
        alert('Passwords must be equals!');
      }
    }
  }

  passwordComparison = () => {
    return this.formData.password;
  }

  // validatePassword(e) {
  //   console.log(this.formData);
  //   if(this.formData.password)
  //     return e.value.equal(this.formData.password);
  // }
}
