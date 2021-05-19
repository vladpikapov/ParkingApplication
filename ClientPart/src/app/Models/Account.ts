import {Wallet} from './Wallet';

export class Account {
  id: number;
  login: string;
  carNumber: string;
  email: string;
  password: string;
  role: any;
  roleId: any;
  confirmEmail: number;
  walletId: number;
  wallet: Wallet = new Wallet();
  createDate: Date;
  lastLogin: Date;
}

export enum RoleEnum {
  User = 1,
  Admin = 2,
  Dispatcher = 3,
}
