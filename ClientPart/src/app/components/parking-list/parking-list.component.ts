import {Component, OnInit, Output} from '@angular/core';
import {ParkingService} from '../../services/parking.service';
import {ParkingRating} from '../../Models/ParkingRating';
import {Parking} from '../../Models/Parking';
import {AuthService} from '../../services/auth.service';
import {SharedService} from '../../services/shared.service';

@Component({
  selector: 'app-parking-list',
  templateUrl: './parking-list.component.html',
  styleUrls: ['./parking-list.component.scss']
})
export class ParkingListComponent implements OnInit {

  dataSource: Parking[];
  visiblePopup = false;
  parkingRatings = [1, 2, 3, 4, 5];
  formData: any;
  selectParking: any;
  orderPopup = false;

  constructor(private op: ParkingService, public as: AuthService, private sharedService: SharedService) {
    this.op.getParking().subscribe(res => {
      this.dataSource = res;
    });

    sharedService.parkingUpdate.subscribe(_ => {
      this.op.getParking().subscribe(res => {
        this.dataSource = res;
      });
    });
  }

  ngOnInit(): void {

  }

  setRating(rating: any): void {
    this.formData = rating;
    this.visiblePopup = !this.visiblePopup;
  }

  saveRating(e): void {
    let rating = new ParkingRating();
    rating.ParkingId = this.formData.id;
    rating.UserRating = e;
    this.op.postRating(rating).subscribe(_ => {
      this.op.getParking().subscribe(res => {
        this.dataSource = res;
      });
    });
    this.visiblePopup = !this.visiblePopup;
  }

  makeOrder(item: any): void {
    this.orderPopup = !this.orderPopup;
    this.selectParking = item;
  }

  showPopup(e) {
    console.log(e);
  }

  orderComplete(): void {
      this.orderPopup = !this.orderPopup;
  }
}
