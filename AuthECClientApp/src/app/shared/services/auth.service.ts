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
    return this.http.post(environment.apiBaseUrl + 'signup', formData);
  }
  signin(formData: any) {
    return this.http.post(environment.apiBaseUrl + 'signin', formData);
  }
  deleteToken() {
    localStorage.removeItem(TOKEN_KEY);
  }
  saveToken(token: string) {
    localStorage.setItem(TOKEN_KEY, token);
  }
  isAuthenticated(): boolean {
    return !!localStorage.getItem(TOKEN_KEY);
  }
  isLoggedIn() {
    return this.getToken() != null ? true : false;
  }
  getToken() {
    return localStorage.getItem(TOKEN_KEY);
  }
  getClaims() {
    return JSON.parse(window.atob(this.getToken()!.split('.')[1]));
  }
}
