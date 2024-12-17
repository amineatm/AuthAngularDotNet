import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient) {}
  baseUrl = 'https://localhost:7183/api/';

  createUser(formData: any) {
    return this.http.post(this.baseUrl + 'signup', formData);
  }
  signin(formData: any) {
    return this.http.post(this.baseUrl + 'signin', formData);
  }
  isAuthenticated(): boolean {
    return !!localStorage.getItem('token'); // Example: Adjust based on your implementation
  }

}
