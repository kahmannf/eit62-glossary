import { Entry } from './../shared/definition/entry';
import { Page } from './../shared/definition/page';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { ApiClientService } from '../shared/api-client.service';

@Component({
  selector: 'gls-add-entry',
  templateUrl: './add-entry.component.html',
  styleUrls: ['./add-entry.component.scss']
})
export class AddEntryComponent implements OnInit {

  formCreate: FormGroup;

  searchResult: Observable<Page<Entry>>;

  constructor(
    private api: ApiClientService
  ) { }

  ngOnInit() {
    this.formCreate = new FormGroup({
      title: new FormControl('', Validators.required),
      description: new FormControl('', Validators.required),
      linkFilter: new FormControl('')
    });

    this.searchResult = this.formCreate.get('linkFilter')
    .valueChanges.pipe(
      switchMap(x => this.api.getEntries(0, 20, x))
    );
  }

}
