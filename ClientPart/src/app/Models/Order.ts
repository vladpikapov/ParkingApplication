import {Parking} from './Parking';

export class Order {
  Id: number;
  OrderStartDate: Date;
  OrderEndDate: Date;
  OrderStatus: OrderStatus;
  OrderUserId: number;
  OrderParkingId: number;
  Parking: Parking;
}

export enum OrderStatus {
  Ok = 1,
  Dined = 2,
  Expired = 3
};
