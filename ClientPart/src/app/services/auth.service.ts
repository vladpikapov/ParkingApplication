import {Inject, Injectable} from '@angular/core';
import {AUTH_API_URL} from "../app-injection-tokens";
import {JwtHelperService} from "@auth0/angular-jwt";
import {Router} from "@angular/router";
import {Observable} from "rxjs";
import {Token} from "../Models/token";
import {HttpClient} from "@angular/common/http";
import {tap} from "rxjs/operators";
import {Account} from "../Models/Account";

export const ACCESS_TOKEN_KEY = 'bookstore_access_token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public currentUser: Account = new Account();

  constructor(private http: HttpClient,
              @Inject(AUTH_API_URL) private apiUrl: string,
              private jwtHelper: JwtHelperService,
              private router: Router) {

  }

  login(email: string, password: string): Observable<Token> {
    return this.http.post<Token>(`${this.apiUrl}api/auth/Login`, {
      email, password
    }).pipe(
      tap(token => {
        localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
        this.setUserData(token.access_token);
        this.router.navigate(['']);
      })
    );
  }

  registration(email: string, password: string): Observable<any> {
    return this.http.post(`${this.apiUrl}api/auth/Registration`, {
      email, password
    });
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    if (!this.currentUser.Email) {
      this.setUserData(token);
    }
    return token && !this.jwtHelper.isTokenExpired(token);
  }

  setUserData(token: any): void {
    const decodeToken = this.jwtHelper.decodeToken(token);
    this.currentUser.Email = decodeToken.email;
    this.currentUser.Role = decodeToken.role;
  }

  logout(): void {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['/login']);
  }
}
