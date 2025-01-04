import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TOKEN_KEY } from '../constants';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}
  createUser(formData: any) {
    return this.http.post(environment.API_BASE_URL + 'AccountIdentity/signup', formData);
  }
  signin(formData: any) {
    return this.http.post(environment.API_BASE_URL + 'AccountIdentity/signin', formData);
  }
}
