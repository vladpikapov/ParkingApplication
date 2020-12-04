import {Inject, Injectable} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {AUTH_API_URL} from "../app-injection-tokens";
import {Parking} from "../Models/Parking";
import {ParkingRating} from "../Models/ParkingRating";

@Injectable({
  providedIn: 'root'
})
export class ParkingService {

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private baseUrl: string) {
  }

  // tslint:disable-next-line:typedef
  getParking() {
    return this.http.get<Parking[]>(`${this.baseUrl}api/Parking/GetParking`);
  }

  getUserParkings() {
    return this.http.get<Parking[]>(`${this.baseUrl}api/Parking/GetUserHistoryParking`);
  }

  postRating(rating: ParkingRating) {
    return this.http.post(`${this.baseUrl}api/Parking/SetParkingRaiting`, rating);
  }
}
