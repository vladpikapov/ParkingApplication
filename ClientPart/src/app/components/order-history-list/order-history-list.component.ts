import {Component, OnInit, ViewChild} from '@angular/core';
import {OrderService} from "../../services/order.service";
import {ParkingService} from "../../services/parking.service";
import {DxFormComponent} from "devextreme-angular";
import {Order} from "../../Models/Order";

@Component({
  selector: 'app-order-history-list',
  templateUrl: './order-history-list.component.html',
  styleUrls: ['./order-history-list.component.scss']
})
export class OrderHistoryListComponent implements OnInit {

  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;

  titleText = '';
  buttonText = '';
  disableStartDate = false;
  dataSource: any[];
  visiblePopup = false;
  minDate = new Date();

  formData = {
    orderId: null,
    orderStartDate: null,
    orderEndDate: null,
    parkingId: null
  };

  constructor(private os: OrderService, private ps: ParkingService) {
  }

  ngOnInit(): void {
    this.os.getOrders().subscribe(res => {
      this.dataSource = res;

    });
  }

  checkDate(rB: any) {
    return new Date(new Date(rB.data.orderEndDate)).getTime() >= new Date().getTime();

  }

  editOrder(e) {
    this.titleText = 'Обновить бронь';
    this.buttonText = 'Обновить';
    this.formData.orderId = e.data.id;
    this.formData.orderStartDate = e.data.orderStartDate;
    this.formData.orderEndDate = e.data.orderEndDate;
    this.formData.parkingId = e.data.orderParkingId;
    this.disableStartDate = !this.disableStartDate;
    this.visiblePopup = !this.visiblePopup;
  }

  postOrder() {
    let order = new Order();
    order.Id = this.formData.orderId;
    order.OrderParkingId = this.formData.parkingId;
    order.OrderStartDate = this.formData.orderStartDate;
    order.OrderEndDate = this.formData.orderEndDate;
    if (!this.disableStartDate) {
      this.pstOrder(order);
    } else {
      this.updOrder(order);
    }
    this.visiblePopup = !this.visiblePopup;

  }

  pstOrder(order: any) {
    this.os.postOrder(order).subscribe(_ => {
      this.os.getOrders().subscribe(res => {
        this.dataSource = res;
      });
    });
  }

  updOrder(order: any) {
    this.os.updateOrder(order).subscribe(_ => {
      this.os.getOrders().subscribe(res => {
        this.dataSource = res;
      });
      this.disableStartDate = false;
    });
  }


  refreshOrder(rB) {
    this.titleText = 'Повторить бронь';
    this.buttonText = 'Повторить';
    this.formData.orderId = rB.data.id;
    this.formData.orderStartDate = null;
    this.formData.orderEndDate = null;
    this.visiblePopup = !this.visiblePopup;
    this.formData.parkingId = rB.data.orderParkingId;
  }

  setDisable($event: any) {


  }
}
