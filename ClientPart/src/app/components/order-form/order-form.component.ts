import {Component, OnInit, ViewChild} from '@angular/core';
import {ParkingService} from "../../services/parking.service";
import {OrderService} from "../../services/order.service";
import {Order} from "../../Models/Order";
import {Router} from "@angular/router";
import {DxFormComponent} from "devextreme-angular";

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit {

  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;

  formData = {
    orderStartDate: null,
    orderEndDate: null,
    parkingId: null
  };

  infoData = {
    orderStartDate: new Date(),
    orderEndDate: new Date(),
    pkId: 0,
    parkingId: {
      city: '',
      address: '',
      capacity: 0
    }
  };

  editorEndDate = {
    value: null,
    width: '100%',
    type: 'datetime',
    showClearButton: 'true',
    useMaskBehavior: 'true',
  };

  allCost: number;
  dataSelectBox: any[];
  selectionChanged = false;
  mainData: any[];
  cities = ['Барановичи', 'Минск'];
  visiblePopup = false;
  minDate = new Date();
  startDate = new Date();

  constructor(private ps: ParkingService, private os: OrderService, private router: Router) {
  }

  ngOnInit(): void {
    this.ps.getParking().subscribe(res => {
      this.mainData = res;
    });
  }

  checkDate(e) {
    if (e.dataField === 'orderStartDate'){
      const endDate = e.component.getEditor('orderEndDate');
      endDate.option({
        onValueChanged(ev: any) {
          endDate.option('min', e.value);
        }
      });
    }
  }

  filterItems(e) {
    this.dataSelectBox = this.mainData.filter(x => e.selectedItem === x.city);
    this.selectionChanged = true;
  }

  checkCost(e) {
    console.log(e);
    console.log(this.formData);
  }

  dateToHours() {

  }

  addOrder() {
    if (this.form && this.form.instance.validate().isValid) {
      if (this.formData.orderStartDate < this.formData.orderEndDate) {
        let date1: string = this.formData.orderStartDate;
        let date2: string = this.formData.orderEndDate;

        let diffInMs: number = Date.parse(date2) - Date.parse(date1);
        let diffInHours: number = diffInMs / 1000 / 60 / 60;
        let diffFixed = diffInHours.toFixed(2);
        this.allCost = Number.parseFloat(this.formData.parkingId.costPerHour) * Number.parseFloat(diffFixed);
        this.infoData.orderEndDate = this.formData.orderEndDate;
        this.infoData.orderStartDate = this.formData.orderStartDate;
        this.infoData.parkingId.city = this.formData.parkingId.city;
        this.infoData.parkingId.address = this.formData.parkingId.address;
        this.infoData.pkId = this.formData.parkingId.id;
        this.infoData.parkingId.capacity = this.formData.parkingId.capacity;
        this.visiblePopup = !this.visiblePopup;
      }
      else {
        alert('Incorrect dates!');
      }
    }
  }

  postOrder() {
    let order = new Order();
    order.OrderStartDate = this.infoData.orderStartDate;
    order.OrderEndDate = this.infoData.orderEndDate;
    order.OrderParkingId = this.infoData.pkId;
    this.os.postOrder(order).subscribe(res => {
      this.router.navigate(['/history']);
    });
  }
}
