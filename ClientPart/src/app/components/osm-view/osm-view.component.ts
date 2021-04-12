import {AfterViewInit, ChangeDetectorRef, Component, EventEmitter, Input, NgZone, Output} from '@angular/core';
import {Map, View} from 'ol';
import {Coordinate} from 'ol/coordinate';
import {defaults as DefaultControls, ScaleLine} from 'ol/control';
import Projection from 'ol/proj/Projection';
import {get as GetProjection} from 'ol/proj';
import {Extent} from 'ol/extent';
import TileLayer from 'ol/layer/Tile';
import OSM from 'ol/source/OSM';
import * as proj4x from 'proj4';
import {register} from 'ol/proj/proj4';

const proj4 = (proj4x as any).default;

@Component({
  selector: 'app-osm-view',
  templateUrl: './osm-view.component.html',
  styleUrls: ['./osm-view.component.scss']
})
export class OsmViewComponent implements AfterViewInit {
  @Input() center: Coordinate;
  @Input() zoom: number;
  view: View;
  projection: Projection;
  extent: Extent = [-20026376.39, -20048966.10,
    20026376.39, 20048966.10];
  Map: Map;
  @Output() mapReady = new EventEmitter<Map>();

  constructor(private zone: NgZone, private cd: ChangeDetectorRef) {
  }


  ngAfterViewInit(): void {
    if (!this.Map) {
      this.zone.runOutsideAngular(() => this.initMap());
    }
    setTimeout(() => this.mapReady.emit(this.Map));
  }

  private initMap(): void {
    proj4.defs('EPSG:3857', '+proj=merc +a=6378137 +b=6378137 +lat_ts=0.0 +lon_0=0.0 +x_0=0.0 +y_0=0 +k=1.0 +units=m +nadgrids=@null +wktext  +no_defs');
    register(proj4);
    this.projection = GetProjection('EPSG:3857');
    this.projection.setExtent(this.extent);
    this.view = new View({
      center: this.center,
      zoom: this.zoom,
      projection: this.projection,
    });
    this.Map = new Map({
      layers: [new TileLayer({
        source: new OSM({})
      })],
      target: 'map',
      view: this.view,
      controls: DefaultControls().extend([
        new ScaleLine({}),
      ]),
    });
  }
}
