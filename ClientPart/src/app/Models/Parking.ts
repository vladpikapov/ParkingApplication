import {ParkingSettings} from './ParkingSettings';

export class Parking {
  id: number;
  address: string;
  latitude: string;
  longitude: string;
  capacity: number;
  freePlaces: number;
  costPerHour: number;
  parkingSettings: ParkingSettings;
}
