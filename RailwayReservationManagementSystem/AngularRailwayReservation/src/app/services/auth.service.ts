import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private apiUrl= "https://localhost:7176/api";

  constructor(private http: HttpClient) { }

  getData(): Observable<any>{
    return this.http.get(`${this.apiUrl}/Passenger/GetAllPassengers`);
  }
}
