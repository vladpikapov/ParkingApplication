import {Component, OnInit} from '@angular/core';
import {ParkingService} from "../../services/parking.service";
import {ParkingRating} from "../../Models/ParkingRating";

@Component({
  selector: 'app-parking-list',
  templateUrl: './parking-list.component.html',
  styleUrls: ['./parking-list.component.scss']
})
export class ParkingListComponent implements OnInit {
  dataSource: any[];
  visiblePopup = false;
  parkingRatings = [1, 2, 3, 4, 5];
  formData: any;

  constructor(private op: ParkingService) {
  }

  ngOnInit(): void {
    this.op.getParking().subscribe(res => {
      this.dataSource = res;
    });
  }

  setRating(rating: any) {
    console.log(rating.data);
    this.formData = rating.data;
    this.visiblePopup = !this.visiblePopup;
  }

  saveRating(e) {
    console.log(e);
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
}
