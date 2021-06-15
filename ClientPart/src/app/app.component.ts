import {Component, ElementRef, HostListener, OnInit, Renderer2} from '@angular/core';
import {AuthService} from './services/auth.service';
import {UserService} from './services/user.service';
import {SharedService} from './services/shared.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public get isLoggedIn(): boolean {
    return this.as.isAuthenticated();
  }

  selectedUser = -1;
  navIsFixed: boolean;
  isLogin = true;
  checkMailPopup = false;
  adminSettingsPopup = false;
  dataSource = [];
  userSettingsPopup = false;
  userBalancePopup: any;

  constructor(public as: AuthService, private el: ElementRef, private renderer: Renderer2, private us: UserService,
              private sharedService: SharedService) {
    this.sharedService.userUpdate.subscribe(_ => {
      this.us.getAllUsers().subscribe(res => {
        this.dataSource = res.filter(x => x.roleId < 2);
      });
    });
    this.sharedService.orderUpdate?.subscribe(_ => {
        if (this.selectedUser > 0) {
          this.sharedService.cartData.emit(this.selectedUser);
        }
      }
    );

  }

  @HostListener('window:scroll', ['$event'])
  onWindowScroll(): void {
    const number = window.pageYOffset ||
      document.documentElement.scrollTop ||
      document.body.scrollTop || 0;
    if (number > 50) {
      this.navIsFixed = true;
    } else if (this.navIsFixed && number < 10) {
      this.navIsFixed = false;
    }
  }


  logout(): void {
    this.as.logout();
  }

  getRole(): string {
    return this.as.currentUser.role;
  }

  getName(): string {
    return this.as.currentUser.login;
  }

  scroll(el: HTMLElement): void {
    el.scrollIntoView({behavior: 'smooth', block: 'center'});
  }

  setLogin(el: any): void {
    this.isLogin = true;
    this.scroll(el);

  }

  setRegistration(el: any): void {
    this.isLogin = false;
    this.scroll(el);
  }

  checkCode(e: any): void {
    this.checkMailPopup = !this.checkMailPopup;
  }

  closePopupMail(): void {
    this.checkMailPopup = !this.checkMailPopup;
    this.isLogin = true;
  }

  openAdminSettings(): void {
    this.adminSettingsPopup = !this.adminSettingsPopup;
  }

  selectUser(event: any): void {
    // this.selectedUser = event.id;
    // this.sharedService.cartData.emit(event.id);
  }

  openUserSettingsPopup(): void {
    this.userSettingsPopup = !this.userSettingsPopup;
  }

  openUserWalletPopup(): void {
    this.userBalancePopup = !this.userBalancePopup;
  }

  ngOnInit(): void {
    // this.us.getAllUsers().subscribe(res => {
    //   this.dataSource = res.filter(x => x.roleId < 2);
      this.sharedService.cartData.emit(true);
    // });
  }

  closeAdminPopup(): void {
    this.sharedService.parkingUpdate.emit();
  }
}
