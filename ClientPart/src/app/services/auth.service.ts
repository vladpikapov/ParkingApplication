import {Inject, Injectable} from '@angular/core';
import {AUTH_API_URL} from "../app-injection-tokens";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {Observable} from "rxjs";
import {Token} from "../Models/token";
import {HttpClient} from "@angular/common/http";
import {tap} from "rxjs/operators";
import {Account} from "../Models/Account";
import {UserService} from "./user.service";

export const ACCESS_TOKEN_KEY = 'bookstore_access_token';
export const CONFIRM_USER = 'confirm_user_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public currentUser: Account = new Account();

  constructor(private http: HttpClient,
              @Inject(AUTH_API_URL) private apiUrl: string,
              private jwtHelper: JwtHelperService,
              private router: Router, private userService: UserService) {

  }

  login(login: string, password: string): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}api/auth/Login`, {
      login, password
    }).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
        localStorage.setItem(CONFIRM_USER, token.confirm_email.toString());
        this.setUserData(token.access_token);
        // this.router.navigate(['']);
      })
    );
  }

  registration(login: string, password: string, email: string, carNumber: string): Observable<any> {
    return this.http.post(`${this.apiUrl}api/auth/Registration`, {
      login, password, email, carNumber
    });
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    const confirmEmail = localStorage.getItem(CONFIRM_USER);
    if (!this.currentUser.login && token) {
      this.setUserData(token);
    }
    return token && !this.jwtHelper.isTokenExpired(token) && confirmEmail === '1';
  }

  isConfirmEmail(): boolean {
    const confirmEmail = localStorage.getItem(CONFIRM_USER);
    return confirmEmail === '1';
  }

  setUserData(token: any): void {
    const decodeToken = this.jwtHelper.decodeToken(token);
    if (decodeToken) {
      this.currentUser.id = Number.parseInt(decodeToken.sub, 0);
      this.currentUser.login = decodeToken.email;
      this.currentUser.role = decodeToken.role;
      setTimeout(() => {
        this.userService.getUserWallet(this.currentUser.id).subscribe(res => this.currentUser.wallet = res);
      }, 0);
    }
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['/login']);
  }
}
