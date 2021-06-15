import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AUTH_API_URL} from '../app-injection-tokens';
import {Parking} from '../Models/Parking';
import {ParkingRating} from '../Models/ParkingRating';
import {Observable} from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ParkingService {

  constructor(private http: HttpClient, @Inject(AUTH_API_URL) private baseUrl: string) {
  }

  getParking(): Observable<any> {
    return this.http.get<Parking[]>(`${this.baseUrl}api/Parking/GetParking`);
  }

  postRating(rating: ParkingRating): Observable<any> {
    return this.http.post(`${this.baseUrl}api/Parking/SetParkingRaiting`, rating);
  }

  createParking(parking: Parking): Observable<any> {
    return this.http.post(`${this.baseUrl}api/AParking/CreateParking`, parking);
  }

  updateParking(parking: Parking): Observable<any> {
    return this.http.put(`${this.baseUrl}api/AParking/UpdateParking`, parking);
  }

  deleteParking(parkingId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}api/AParking/DeleteParking/${parkingId}`);
  }

}
