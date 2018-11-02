import { Entry } from './../shared/definition/entry';
import { Observable, of } from 'rxjs';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'gls-entry-list',
  templateUrl: './entry-list.component.html',
  styleUrls: ['./entry-list.component.scss']
})
export class EntryListComponent implements OnInit {

  @Input()
  entries$: Observable<Entry[]>;

  constructor() { }

  ngOnInit() {

    const entries: Entry[] = [
      {
        Title: 'Rekursion',
        Description: 'Siehe Rekursion',
        Guid: '1234',
        References: [
          {
            Title: 'Rekursion',
            Guid: '1234'
          }
        ]
      },
    ];

    for (let i = 0; i < 10; i++) {

      entries.push(
        {
          Title: 'Entry ' + i,
          Description: 'Test bestreibung wjeöofiash awüe8zhföasd iuwegsldf §GD LAUIWEFAWL EG/RGFEWF ' +
          '  ugaIGIDILDGFCSDHV ÖE  LEGWFLAGWGEF   UGEWoeöstgh  aowoguf sadf',
          Guid: 'entry' + i,
          References: [
          ]
        });

    }

    entries.push(
      {
        Title: 'Blaaa',
        Description: 'Test bestreibung',
        Guid: '4321',
        References: [
          {
            Title: 'Rekursion',
            Guid: '1234'
          }
        ]
      });


    this.entries$ = of(entries);

  }

}
