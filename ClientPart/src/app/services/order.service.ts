import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Order} from "../Models/Order";
import {AUTH_API_URL} from "../app-injection-tokens";

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private apiUrl: string) {
  }

  getOrders() {
    return this.http.get<Order[]>(`${this.apiUrl}api/Order/GetOrders`);
  }

  postOrder(order: Order) {
    return this.http.post(`${this.apiUrl}api/Order/PostOrder`, order);
  }

  // tslint:disable-next-line:typedef
  updateOrder(order: Order){
    return this.http.put(`${this.apiUrl}api/Order/UpdateOrder`, order);
  }
}
