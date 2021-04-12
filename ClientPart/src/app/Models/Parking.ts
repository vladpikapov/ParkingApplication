import {ParkingSettings} from './ParkingSettings';

export class Parking {
  Id: number;
  Address: string;
  City: string;
  Latitude: number;
  Longitude: number;
  Capacity: number;
  CostPerHour: number;
  ParkingSettings: ParkingSettings;
}
