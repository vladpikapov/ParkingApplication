import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {UserService} from '../../services/user.service';
import {AuthService} from '../../services/auth.service';
import {Router} from '@angular/router';
import {Account} from '../../Models/Account';

@Component({
  selector: 'app-check-email',
  templateUrl: './check-email.component.html',
  styleUrls: ['./check-email.component.scss']
})
export class CheckEmailComponent implements OnInit {
  @Input()
  isSettings = false;

  @Input()
  account: Account;

  @Output()
  closePopup = new EventEmitter();

  @Input()
  popupVisible: boolean;

  userCode: number;

  constructor(private router: Router, private us: UserService, private as: AuthService) {
  }

  ngOnInit(): void {
  }


  ConfirmEmail(): void {
    this.us.sendUserCode(this.userCode, this.as.currentUser.login).subscribe(res => {
      if (res) {
        this.router.navigate(['/login']);
        this.closePopup.emit();
      } else {
        alert('Неверный код');
      }

    });
  }

  updateEmail(): void {
    this.us.updateUserMail(this.account, this.userCode).subscribe(res => {
      if (res) {
        this.popupVisible = !this.popupVisible;
        this.closePopup.emit();
        alert('Success !');
      } else {
        alert('Wrong code !');
      }
    });
  }
}
