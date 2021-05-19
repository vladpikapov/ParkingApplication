import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {OrderService} from "../../services/order.service";
import {ParkingService} from "../../services/parking.service";
import {DxFormComponent} from "devextreme-angular";
import {Order} from "../../Models/Order";
import {SharedService} from "../../services/shared.service";
import {UserService} from "../../services/user.service";
import {Wallet} from "../../Models/Wallet";

@Component({
  selector: 'app-order-history-list',
  templateUrl: './order-history-list.component.html',
  styleUrls: ['./order-history-list.component.scss']
})
export class OrderHistoryListComponent implements OnInit {

  @Input()
  userId = -1;


  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;

  selectedOrder = new Order();
  endDate: any;
  titleText = '';
  buttonText = '';
  disableStartDate = false;
  dataSource: any[];
  visiblePopup = false;
  minDate = new Date();

  formData = new Order();

  constructor(private os: OrderService, private ps: ParkingService, private sharedService: SharedService, private us: UserService) {
    sharedService.cartData.subscribe(n => {
      if (n) {
        this.os.getUserOrders(n).subscribe(res => {
          this.dataSource = res;
        });
      } else {
        this.os.getOrders().subscribe(res => {
          this.dataSource = res;
        });
      }
    })
  }

  ngOnInit(): void {
    if (this.userId === -1) {
      this.os.getOrders().subscribe(res => {
        this.dataSource = res;
      });
    } else {
      this.os.getUserOrders(this.userId).subscribe(res => {
        this.dataSource = res;
      });
    }
  }

  checkDate(rB: any): boolean {
    return new Date(new Date(rB.data.orderEndDate)).getTime() >= new Date().getTime();

  }

  editOrder(e): void {
    this.titleText = 'Обновить бронь';
    this.buttonText = 'Обновить';
    this.formData = e.data;
    this.selectedOrder.orderEndDate = e.data.orderEndDate;
    this.disableStartDate = !this.disableStartDate;
    this.visiblePopup = !this.visiblePopup;
  }

  postOrder(): void {
    let wallet = new Wallet();
    let allCost = 0;
    const date1: string = this.selectedOrder.orderEndDate;
    const date2: string = this.formData.orderEndDate;

    const diffInMs: number = Date.parse(date2) - Date.parse(date1);
    const diffInHours: number = diffInMs / 1000 / 60 / 60;
    const diffFixed = diffInHours.toFixed(2);
    allCost = this.formData.parking.costPerHour * Number.parseFloat(diffFixed);
    this.us.getUserWallet(this.formData.orderUserId).subscribe(res => {
      wallet = res;
    });
    if (new Date(this.endDate).getTime() > new Date(this.formData.orderEndDate).getTime()) {
      alert('Incorrect date!');
    } else if (wallet.moneySum < allCost) {
      alert('Insufficient funds');
    } else {
      this.updOrder(this.formData);
      this.sharedService.orderUpdate.emit();
      // setTimeout(() => {
      //   wallet.moneySum -= allCost;
      //   this.us.updateUserWallet(wallet).subscribe(() => {
      //   });
      // }, 0);

    }

  }

  pstOrder(order: any): void {
    this.os.postOrder(order).subscribe(_ => {
      this.os.getOrders().subscribe(res => {
        this.dataSource = res;
      });
    });
  }

  updOrder(order: any): void {
    this.os.updateOrder(order).subscribe(_ => {
      this.os.getOrders().subscribe(res => {
        this.dataSource = res;
        this.sharedService.cartData.emit();
        this.visiblePopup = !this.visiblePopup;
      });
      this.disableStartDate = false;
    });
  }


  refreshOrder(rB): void {
    this.titleText = 'Повторить бронь';
    this.buttonText = 'Повторить';
    this.formData.orderId = rB.data.orderId;
    this.formData.orderUserId = rB.data.orderUserId;
    this.formData.orderStartDate = null;
    this.formData.orderEndDate = null;
    this.visiblePopup = !this.visiblePopup;
    this.formData.orderParkingId = rB.data.orderParkingId;
  }

  setDefault(): void {
    this.disableStartDate = false;
  }

  setMinDate(e: any): void {
    if (e.dataField === 'orderStartDate') {
      const endDate = e.component.getEditor('orderEndDate');
      endDate.option({
        onValueChanged(ev: any): void {
          endDate.option('min', e.value);
        }
      });
    }
  }

  deleteOrder(dB: any): void {
    this.os.deleteOrder(dB.data.orderId).subscribe(_ => {
      this.sharedService.cartData.emit();
    });
  }
}
