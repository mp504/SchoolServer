import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'http://localhost:5188/api/Account/login';
  constructor(private http: HttpClient) { }

  login(email: string, password: string): Observable<any>
  {

    return this.http.post(this.apiUrl, { email, password });

  }

  // Additional method for checking if the user is logged in
  isAuthenticated(): boolean {
    return !!localStorage.getItem('token');
  }
  // Method to log out
  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('Id');
  }
}
