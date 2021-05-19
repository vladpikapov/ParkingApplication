import {Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
import {NominatimResponse} from '../../../Models/MapModels/NominatimResponse';

@Component({
  selector: 'app-results-list',
  templateUrl: './results-list.component.html',
  styleUrls: ['./results-list.component.scss']
})
export class ResultsListComponent implements OnInit {

  @Input()
  results: NominatimResponse[];

  @Output()
  locationSelected = new EventEmitter<any>();

  constructor() {
  }

  selectResult(result: NominatimResponse): void {
    this.locationSelected.emit(result);
  }

  ngOnInit(): void {
  }
}
