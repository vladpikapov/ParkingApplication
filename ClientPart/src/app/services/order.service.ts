import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Order} from "../Models/Order";
import {AUTH_API_URL} from "../app-injection-tokens";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private apiUrl: string) {
  }

  getAllOrders(): Observable<Order[]> {
    return this.http.get<Order[]>(`${this.apiUrl}api/AOrder/GetOrders/`);
  }

  getOrders(): Observable<any> {
    return this.http.get<Order[]>(`${this.apiUrl}api/Order/GetOrders/`);
  }

  postOrder(order: Order): Observable<any> {
    return this.http.post(`${this.apiUrl}api/Order/PostOrder`, order);
  }

  postOrderByAdmin(order: Order): Observable<any> {
    return this.http.post(`${this.apiUrl}api/AOrder/PostOrder`, order);
  }

  updateOrder(order: Order): Observable<any> {
    return this.http.put(`${this.apiUrl}api/Order/UpdateOrder`, order);
  }

  getUserOrders(userId: number): Observable<any> {
    return this.http.get<Order[]>(`${this.apiUrl}api/AOrder/GetUserOrders/${userId}`);
  }

  deleteOrder(orderId: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}api/Order/DeleteOrder/${orderId}`);
  }
}
