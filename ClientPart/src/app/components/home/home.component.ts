import {MapsAPILoader} from '@agm/core';
import {Component, OnInit} from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private apiLoader: MapsAPILoader) {
  }

  lat: any;
  lng: any;
  getAddress: any;

  ngOnInit(): void {
    this.get();
  }

  get() {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position: Position) => {
        if (position) {
          this.lat = position.coords.latitude;
          this.lng = position.coords.longitude;
        }
      });
    }
  }
}
