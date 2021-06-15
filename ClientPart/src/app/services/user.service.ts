import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Account} from '../Models/Account';
import {AUTH_API_URL} from '../app-injection-tokens';
import {Observable} from 'rxjs';
import {Wallet} from '../Models/Wallet';
import {Order} from "../Models/Order";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private apiUrl: string) {
  }

  getUser(userId: number): Observable<Account> {
    return this.http.get<Account>(`${this.apiUrl}api/User/GetAccount/${userId}`);
  }

  getUserWallet(userId: number): Observable<Wallet> {
    return this.http.get<Wallet>(`${this.apiUrl}api/User/GetUserWallet/${userId}`);
  }

  updateUserWallet(wallet: Wallet): Observable<any> {
    return this.http.put(`${this.apiUrl}api/User/UpdateUserWallet`, wallet);
  }

  sendUserCode(code: number, login: string): Observable<boolean> {
    return this.http.get<boolean>(`${this.apiUrl}api/User/SendUserCode/${code}/${login}`);
  }

  updateUserData(account: Account, updateType: number): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}api/User/UpdateUserData/${updateType}`, account);
  }

  updateUserMail(account: Account, code: number): Observable<boolean> {
    return this.http.put<boolean>(`${this.apiUrl}api/User/UpdateUserMail/${code}`, account);
  }
  getAllUsers(): Observable<Account[]> {
    return this.http.get<Account[]>(`${this.apiUrl}api/AUser/GetAccounts/`);
  }

  getUserOrders(userId: number): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}api/AOrder/GetUserOrders/${userId}`);
  }

  sendCodeToMail(mail: string): Observable<any> {
    return this.http.get(`${this.apiUrl}api/User/SendCodeToMail/${mail}`);
  }

  deleteUser(userId: number): Observable<any> {
    return this.http.get(`${this.apiUrl}api/AUser/DeleteUser/${userId}`);
  }
}

