import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  getUserProfile() {
    return this.http.get(environment.API_BASE_URL + 'Account/userProfile');
  }
  getUserRoles() {
    return this.http.get(environment.API_BASE_URL + 'Account/UserRoles');
  }
}
