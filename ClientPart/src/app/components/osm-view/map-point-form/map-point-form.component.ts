import {Component, Input, OnInit} from '@angular/core';
import {MapPoint} from '../../../Models/MapModels/MapPoint';

@Component({
  selector: 'app-map-point-form',
  templateUrl: './map-point-form.component.html',
  styleUrls: ['./map-point-form.component.scss']
})
export class MapPointFormComponent implements OnInit {

  @Input()
  mapPoint: MapPoint = new MapPoint();

  constructor() {
  }

  ngOnInit(): void {
  }

}
