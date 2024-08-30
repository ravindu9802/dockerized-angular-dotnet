import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);
  private API_BASE_URL = environment.API_BASE_URL + 'auth';

  constructor() {}

  validateLogin(email: string, password: string) {
    return this.http.post(
      this.API_BASE_URL,
      {
        email: email,
        password: password,
      },
      { observe: 'response' }
    );
  }

  isLogged() {
    const isLogged = localStorage.getItem('isLogged');
    if (isLogged == 'TRUE') {
      return true;
    }
    return false;
  }
}
