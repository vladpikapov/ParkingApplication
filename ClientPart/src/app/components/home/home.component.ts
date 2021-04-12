import {Component, OnInit} from '@angular/core';

declare var ol: any;

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  latitude = 0;
  longitude = 0;

  onMapReady($event): void {
    console.log($event);
    console.log('Map ready');

  }

  constructor() {
    navigator.geolocation.getCurrentPosition(x => {
      this.latitude = x.coords.latitude;
      this.longitude = x.coords.longitude;
      console.log(this.latitude);
      console.log(this.longitude);
    });
  }

  ngOnInit(): void {

  }
}
