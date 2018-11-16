import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Page } from 'src/app/shared/definition/page';

@Component({
  selector: 'gls-page-navigator',
  templateUrl: './page-navigator.component.html',
  styleUrls: ['./page-navigator.component.scss']
})
export class PageNavigatorComponent implements OnInit {

  constructor() { }

  @Input()
  disable: boolean;

  @Input()
  page: Page<any>;

  @Output()
  next = new EventEmitter<any>();

  @Output()
  previous = new EventEmitter<any>();

  ngOnInit() {
  }

  canNextPage(): boolean {
    return this.page && ((this.page.Index + 1) * this.page.Size < this.page.Total);
  }

  canPreviousPage(): boolean {
    return this.page && this.page.Index > 0;
  }

}
