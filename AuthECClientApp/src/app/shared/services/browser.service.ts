import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TOKEN_KEY } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class BrowserService {

  constructor(private http: HttpClient) { }
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
