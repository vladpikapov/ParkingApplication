import {EventEmitter, Injectable} from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  cartData = new EventEmitter<any>();

  parkingUpdate = new EventEmitter<any>();

  orderUpdate = new EventEmitter<any>();

  userUpdate = new EventEmitter<any>();

  parkingEdit = new EventEmitter<any>();

  parkingNew = new EventEmitter<any>();

  constructor() {
  }
}
