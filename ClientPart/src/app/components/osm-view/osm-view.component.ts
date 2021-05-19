import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {MapPoint} from '../../Models/MapModels/MapPoint';
import {icon, latLng, LeafletMouseEvent, Map, MapOptions, marker, tileLayer} from 'leaflet';
import {NominatimResponse} from '../../Models/MapModels/NominatimResponse';
import {Parking} from '../../Models/Parking';
import {ParkingService} from '../../services/parking.service';
import {SharedService} from '../../services/shared.service';


@Component({
  selector: 'app-oms-view',
  templateUrl: './osm-view.component.html',
  styleUrls: ['./osm-view.component.scss']
})
export class OsmViewComponent implements OnInit {
  @Input()
  isUser: boolean;

  @Input()
  isEdit: boolean;
  map: Map;
  mapPoint: MapPoint;
  options: MapOptions;
  lastLayer: any;

  @Output()
  mapPointChange = new EventEmitter<any>();

  results: NominatimResponse[];

  constructor(private op: ParkingService, private sharedService: SharedService) {
    sharedService.parkingUpdate.subscribe(_ => {
      this.clearMap();
      this.op.getParking().subscribe(res => {
        res.forEach(x => {
          this.createParkingMarker(x);
        });
      });
    });
    sharedService.parkingEdit.subscribe(res => {
      this.clearMap();
      this.createParkingMarker(res);
    });
    sharedService.parkingNew.subscribe(_ => {
      this.clearMap();
    });
  }

  ngOnInit(): void {
    this.initializeDefaultMapPoint();
    this.initializeMapOptions();
  }

  initializeMap(map: Map): void {
    this.map = map;
    this.createMarker();
    this.clearMap();
    if (this.isUser) {
      this.op.getParking().subscribe(res => {
        res.forEach(x => {
          this.createParkingMarker(x);
        });
      });
    }
  }

  getAddress(result: NominatimResponse): void {
    this.updateMapPoint(result.latitude, result.longitude, result.displayName);
    if (!this.isUser) {
      this.createMarker();
    }
  }

  refreshSearchList(results: NominatimResponse[]): void {
    this.results = results;
  }

  onMapClick(e: LeafletMouseEvent): void {
    if (!this.isUser) {
      this.updateMapPoint(e.latlng.lat, e.latlng.lng);
      this.createMarker();
    }
  }

  private initializeMapOptions(): void {
    this.options = {
      zoom: 12,
      layers: [
        tileLayer('http://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {maxZoom: 18, attribution: 'OSM'})
      ]
    };
  }

  private initializeDefaultMapPoint(): void {
    this.mapPoint = {
      name: '',
      latitude: 53.902334,
      longitude: 27.5618791,
      message: 'hello world!'
    };
  }

  private updateMapPoint(latitude: number, longitude: number, name?: string): void {
    this.mapPoint = {
      latitude,
      longitude,
      name: name ? name : this.mapPoint.name,
      message: ''
    };
    this.mapPointChange.emit(this.mapPoint);
  }

  private createParkingMarker(parking: Parking): void {
    const mapIcon = this.getDefaultIcon();
    const coordinates = latLng([Number.parseFloat(parking.latitude), Number.parseFloat(parking.longitude)]);
    this.lastLayer = marker(coordinates).bindPopup(parking.address).setIcon(mapIcon).addTo(this.map);
    // this.map.setView(coordinates, this.map.getZoom());
  }

  private createMarker(): void {
    this.clearMap();
    const mapIcon = this.getDefaultIcon();
    const coordinates = latLng([this.mapPoint.latitude, this.mapPoint.longitude]);
    this.lastLayer = marker(coordinates).bindPopup('').setIcon(mapIcon).addTo(this.map);
    this.map.setView(coordinates, this.map.getZoom());
  }

  private getDefaultIcon(): any {
    return icon({
      iconSize: [25, 41],
      iconAnchor: [13, 41],
      iconUrl: 'assets/marker-icon.png'
    });
  }

  private clearMap(): void {
    if (this.map.hasLayer(this.lastLayer)) {
      this.map.removeLayer(this.lastLayer);
    }
  }
}
