import {Component, OnInit} from '@angular/core';
import {Parking} from '../../../Models/Parking';
import {ParkingService} from '../../../services/parking.service';
import {DxFormComponent} from "devextreme-angular";
import {ParkingSettings} from "../../../Models/ParkingSettings";
import {SharedService} from "../../../services/shared.service";

@Component({
  selector: 'app-parking-form',
  templateUrl: './parking-form.component.html',
  styleUrls: ['./parking-form.component.scss']
})
export class ParkingFormComponent implements OnInit {

  isEdit = false;
  dataSource: Parking[];
  popupVisible = false;
  formData: Parking = new Parking();

  constructor(private op: ParkingService, private sharedService: SharedService) {
  }

  ngOnInit(): void {
    this.op.getParking().subscribe(res => {
      this.dataSource = res;
    });
  }

  createParking(): void {
    this.popupVisible = !this.popupVisible;
    this.sharedService.parkingNew.emit();
  }

  onToolbarPreparing(e): void {
    e.toolbarOptions.items.unshift({
      location: 'after',
      widget: 'dxButton',
      options: {
        icon: 'add',
        onClick: this.createParking.bind(this)
      }
    });
  }

  dropParking(elem: any): void {
    this.op.deleteParking(elem.data.id).toPromise().then(_ => {
      this.op.getParking().toPromise().then(res => {
        this.dataSource = res;
        this.sharedService.parkingUpdate.emit();
        this.sharedService.cartData.emit();
      });
    });
  }

  editParking(elem: any): void {
    this.isEdit = !this.isEdit;
    this.formData = elem.data;
    this.popupVisible = !this.popupVisible;
    this.sharedService.parkingEdit.emit(elem.data);
  }

  mapPointChange(e): void {
    this.formData.address = e.name;
    this.formData.longitude = e.longitude;
    this.formData.latitude = e.latitude;
  }

  saveParking(form: DxFormComponent): void {
    if (form.instance.validate().isValid) {
      const park = new Parking();
      park.parkingSettings = new ParkingSettings();
      park.id = this.formData.id;
      park.address = this.formData.address;
      park.capacity = this.formData.capacity;
      park.latitude = this.formData.latitude;
      park.longitude = this.formData.longitude;
      park.costPerHour = this.formData.costPerHour;
      park.parkingSettings.parkingId = this.formData.parkingSettings.parkingId;
      park.parkingSettings.allTimeService = this.formData.parkingSettings.allTimeService ? 1 : 0;
      park.parkingSettings.cctv = this.formData.parkingSettings.cctv ? 1 : 0;
      park.parkingSettings.forPeopleWithDisabilities = this.formData.parkingSettings.forPeopleWithDisabilities ? 1 : 0;
      park.parkingSettings.leaveTheCarKeys = this.formData.parkingSettings.leaveTheCarKeys ? 1 : 0;
      if (!this.isEdit) {
        this.op.createParking(park).subscribe(_ => {
          this.op.getParking().subscribe(x => {
            this.dataSource = x;
            this.sharedService.parkingUpdate.emit();
          });
        });
      } else {
        this.op.updateParking(park).subscribe(_ => {
          this.op.getParking().subscribe(x => {
            this.dataSource = x;
            this.sharedService.parkingUpdate.emit();
          });
          this.isEdit = false;
        });
      }
      this.formData = new Parking();
      this.popupVisible = !this.popupVisible;

    }
  }

  closePopup(parkingForm: DxFormComponent): void {
    // parkingForm.instance.resetValues();
    this.formData = new Parking();
  }

  canDeleteParking(elem: any): boolean {
    // let check = false;
    // this.op.canDeleteParking(elem.data.id).subscribe(res =>
    // {
    //   check = res;
    // });
    // return check;
    return false;
  }
}
