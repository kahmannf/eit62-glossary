import { Injectable, EventEmitter } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class EventService {

  loginChanged: EventEmitter<undefined> = new EventEmitter<undefined>();

  constructor() { }
}
