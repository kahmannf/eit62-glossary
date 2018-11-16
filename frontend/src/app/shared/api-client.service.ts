import { Entry } from './definition/entry';
import { Page } from './definition/page';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { LoginResponse } from './definition/login-response';

@Injectable({
  providedIn: 'root'
})
export class ApiClientService {

  readonly API_URL = 'http://localhost/GlossaryBackend/';

  constructor(private http: HttpClient) { }

  login(dataBase64: string): Observable<LoginResponse> {
    return this.http.get<LoginResponse>(
      this.API_URL + 'api/auth/login',
      { headers: { 'Authorization': 'Basic ' + dataBase64 } }
    );
  }

  getEntries(index?: number, maxSize?: number, search?: string): Observable<Page<Entry>> {
    return this.http.get<Page<Entry>>(
      this.API_URL + 'api/entries?index=' +
      (index ? index : 0) +
      (maxSize ? '&maxSize=' + maxSize : '') +
      (search ? '&search=' + encodeURI(search) : '')
    );
  }

  addEntry(entry: Entry): Observable<any> {
    return this.http.post(
      this.API_URL + 'api/entries',
      entry
    );
  }
}
