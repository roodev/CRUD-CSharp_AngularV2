import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CustomerloanService {

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:5014/Customer/getcustomers');
  }

  deleteCustomer(id: number): Observable<any> {
    const url = `http://localhost:5014/Customer/${id}`;
    return this.http.delete<any>(url);
  }
  
  createCustomer(customer: any): Observable<any> {
    return this.http.post<any>('http://localhost:5014/Customer', customer);
  }
  

  getCustomerById(id: number): Observable<any> {
    const url = `http://localhost:5014/Customer/getcustomerbyid/${id}`;
    return this.http.get<any>(url);
  }

  updateCustomer(customer: any): Observable<any> {
    return this.http.put<any>('http://localhost:5014/Customer', customer);
  }
}
