import {Component, OnInit} from '@angular/core';
import {AuthService} from "../../services/auth.service";
import {UserService} from "../../services/user.service";
import {Wallet} from "../../Models/Wallet";

@Component({
  selector: 'app-user-balance',
  templateUrl: './user-balance.component.html',
  styleUrls: ['./user-balance.component.scss']
})
export class UserBalanceComponent implements OnInit {

  userWallet: Wallet = new Wallet();
  newSum = 0;
  carNumber: any;
  carHolder: any;
  cardM: any;
  cardY: any;
  cvv: any;

  constructor(public as: AuthService, private userService: UserService) {
    if (this.as.currentUser) {
    }
  }

  ngOnInit(): void {
    this.userService.getUserWallet(this.as.currentUser.id).subscribe(res => this.userWallet = res);
  }

  updateMoney(): void {
    this.userWallet.moneySum += this.newSum;
    this.userService.updateUserWallet(this.userWallet).subscribe(() => {
      this.userService.getUserWallet(this.as.currentUser.id).subscribe(res => {
        this.userWallet = res;
        this.as.currentUser.wallet = res;
      });
    });
  }
}
