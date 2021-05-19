import {Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {ParkingService} from '../../services/parking.service';
import {OrderService} from '../../services/order.service';
import {Order} from '../../Models/Order';
import {Router} from '@angular/router';
import {DxFormComponent} from 'devextreme-angular';
import {Parking} from '../../Models/Parking';
import {UserService} from '../../services/user.service';
import {AuthService} from '../../services/auth.service';
import {Wallet} from '../../Models/Wallet';
import {SharedService} from "../../services/shared.service";

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html',
  styleUrls: ['./order-form.component.scss']
})
export class OrderFormComponent implements OnInit {

  @Input()
  selectedParking: Parking;

  @Output()
  orderComplete = new EventEmitter();

  @ViewChild(DxFormComponent, {static: false})
  form: DxFormComponent;

  formData = {
    userId: null,
    orderStartDate: null,
    orderEndDate: null,
  };

  infoData = {
    orderStartDate: '',
    orderEndDate: '',
    pkId: 0,
    parkingId: {
      city: '',
      address: '',
      capacity: 0,
      freePlaces: 0,
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
  parking = [];
  visiblePopup = false;
  minDate = new Date();
  startDate = new Date();

  constructor(private ps: ParkingService, private os: OrderService, private router: Router,
              private us: UserService, public as: AuthService, private sharedService: SharedService) {
      this.us.getAllUsers().toPromise().then(res => {
        this.dataSelectBox = res.filter(x => x.roleId < 2);
    });
  }

  ngOnInit(): void {
    this.ps.getParking().subscribe(res => {
      this.mainData = res;
    });
  }

  checkDate(e): void {
    if (e.dataField === 'orderStartDate') {
      const endDate = e.component.getEditor('orderEndDate');
      endDate.option({
        onValueChanged(ev: any): void {
          endDate.option('min', e.value);
        }
      });
    }
  }

  addOrder(): void {
    if (this.form && this.form.instance.validate().isValid) {
      if (this.formData.orderStartDate < this.formData.orderEndDate) {
        const date1: string = this.formData.orderStartDate;
        const date2: string = this.formData.orderEndDate;

        const diffInMs: number = Date.parse(date2) - Date.parse(date1);
        const diffInHours: number = diffInMs / 1000 / 60 / 60;
        const diffFixed = diffInHours.toFixed(2);
        this.allCost = this.selectedParking.costPerHour * Number.parseFloat(diffFixed);
        this.allCost = Math.round(this.allCost);
        this.infoData.orderEndDate = this.formData.orderEndDate;
        this.infoData.orderStartDate = this.formData.orderStartDate;
        this.infoData.parkingId.address = this.selectedParking.address;
        this.infoData.pkId = this.selectedParking.id;
        this.infoData.parkingId.capacity = this.selectedParking.capacity;
        this.infoData.parkingId.freePlaces = this.selectedParking.freePlaces;
        this.visiblePopup = !this.visiblePopup;
      } else {
        alert('Incorrect dates!');
      }
    }
  }

  postOrder(): void {
    const order = new Order();
    order.orderStartDate = this.infoData.orderStartDate;
    order.orderEndDate = this.infoData.orderEndDate;
    order.orderParkingId = this.infoData.pkId;
    order.allCost = this.allCost;
    let userWallet = new Wallet();
    if (this.formData.userId) {
      this.us.getUserWallet(this.formData.userId.id).toPromise().then(res => {
        userWallet = res;
      });
    }
    if ((this.allCost <= userWallet.moneySum && this.formData.userId) || this.as.currentUser.wallet.moneySum >= this.allCost) {
      if (this.formData.userId) {
        order.orderUserId = Number.parseFloat(this.formData.userId.id);
        this.os.postOrderByAdmin(order).subscribe(_ => {
          this.sharedService.cartData.emit();
          this.orderComplete.emit();
          this.visiblePopup = !this.visiblePopup;
        });
      } else {
        this.os.postOrder(order).subscribe(_ => {
          this.us.getUserWallet(this.as.currentUser.id).toPromise().then(res => {
            this.as.currentUser.wallet = res;
            this.sharedService.cartData.emit();
            this.visiblePopup = !this.visiblePopup;
            this.orderComplete.emit();
          });
        });
      }
    } else {
      alert('Insufficient funds');
    }
  }
}
