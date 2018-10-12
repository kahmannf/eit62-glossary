import { Injectable } from '@angular/core';
import { ApiClientService } from './api-client.service';
import { EventService } from './event.service';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(
    private api: ApiClientService,
    private events: EventService
  ) { }

  login(email: string, password: string) {
    const base64Authdata = btoa(`${email} ${password}`);

    this.api.login(base64Authdata).subscribe(result => {

      localStorage.setItem('token', result.access_token);
      localStorage.setItem('refresh_token', result.refresh_token);

      this.events.loginChanged.emit(undefined);
    });
  }


}
