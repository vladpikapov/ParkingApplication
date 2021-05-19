import {Parking} from './Parking';
import {Account} from './Account';

export class Order {
  orderId: number;
  orderStartDate: string;
  orderEndDate: string;
  orderStatus: OrderStatus;
  account: Account;
  orderUserId: number;
  orderParkingId: number;
  parking: Parking;
  allCost: number;
}

export enum OrderStatus {
  Ok = 1,
  Dined = 2,
  Expired = 3
}

