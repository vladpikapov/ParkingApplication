import {Component, OnInit} from '@angular/core';
import {UserService} from '../../services/user.service';
import {AuthService} from '../../services/auth.service';
import {SharedService} from '../../services/shared.service';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
  dataSource: any[];

  constructor(private us: UserService, private as: AuthService, private sharedService: SharedService) {
    this.sharedService.userUpdate.subscribe(_ => {
      this.us.getAllUsers().subscribe(res => {
        this.dataSource = res;
      }, error => {});
    });
  }

  ngOnInit(): void {
    this.us.getAllUsers().subscribe(res => {
      this.dataSource = res;
    }, error => {});
  }

  deleteUser(data: any): void {
    console.log(data);
  }

  checkUserData(data: any): boolean {
    if (data.data.roleId === 2) {
      return true;
    }
    return false;
  }
}
