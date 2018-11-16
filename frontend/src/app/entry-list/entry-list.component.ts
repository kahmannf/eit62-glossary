import { Entry } from './../shared/definition/entry';
import { Observable, of } from 'rxjs';
import { Component, OnInit, Input } from '@angular/core';
import { ApiClientService } from '../shared/api-client.service';
import { Page } from '../shared/definition/page';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'gls-entry-list',
  templateUrl: './entry-list.component.html',
  styleUrls: ['./entry-list.component.scss']
})
export class EntryListComponent implements OnInit {

  @Input()
  entryPage$: Observable<Page<Entry>>;

  constructor(
    private api: ApiClientService
  ) { }

  ngOnInit() {
    this.refresh();
  }

  refresh() {
    this.entryPage$ = this.api.getEntries(0, 3);
  }

  nextPage() {
    this.entryPage$ = this.
  }

  previousPage() {

  }

}
