import {Component, OnInit, ViewChild} from '@angular/core';
import {DxFormComponent} from 'devextreme-angular';
import {Router} from '@angular/router';
import {AuthService} from '../../services/auth.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;

  formData = {
    mail: '',
    password: '',
    repeatPassword: ''
  };

  constructor(private router: Router, private as: AuthService) {

  }

  ngOnInit(): void {
  }

  registration() {
    if (this.form && this.form.instance.validate().isValid) {
      console.log('here');
      this.as.registration(this.formData.mail, this.formData.password).subscribe(res => {
        if (res === false) {
          alert('Such a user already exist!');
        }
        else {
          this.router.navigate(['/login']);
        }
      });
    }
  }

  // validatePassword(e) {
  //   console.log(this.formData);
  //   if(this.formData.password)
  //     return e.value.equal(this.formData.password);
  // }
}
