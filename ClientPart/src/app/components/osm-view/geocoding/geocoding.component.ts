import {Component, EventEmitter, Output} from '@angular/core';
import {NominatimResponse} from '../../../Models/MapModels/NominatimResponse';
import {NominatimService} from '../../../services/nominatim.service';

@Component({
  selector: 'app-geocoding',
  templateUrl: './geocoding.component.html',
  styleUrls: ['./geocoding.component.scss']
})
export class GeocodingComponent {

  @Output() onSearch = new EventEmitter<any>();
  searchResults: NominatimResponse[];

  constructor(private nominatimService: NominatimService) {
  }

  addressLookup(address: string): void {
    if (address.length > 3) {
      this.nominatimService.addressLookup(address).subscribe(results => {
        this.searchResults = results;
      });
    } else {
      this.searchResults = [];
    }
    this.onSearch.emit(this.searchResults);
  }

}
