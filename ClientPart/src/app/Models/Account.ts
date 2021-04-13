import {Wallet} from './Wallet';

export class Account {
  Id: number;
  Email: string;
  Password: string;
  Role: any;
  WalletId: number;
  Wallet: Wallet;
}

export enum RoleEnum {
  User = 1,
  Admin = 2,
  Dispatcher = 3,
}
