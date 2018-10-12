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
}
