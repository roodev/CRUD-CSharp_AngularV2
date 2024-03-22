import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse  } from '@angular/common/http';
import { Observable, throwError  } from 'rxjs';
import { catchError, timeout } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CustomerloanService {
  private customersUrl = 'http://localhost:5014/Customer';
  private currenciesUrl = 'https://olinda.bcb.gov.br/olinda/servico/PTAX/versao/v1/odata/Moedas?$top=100&$format=json';

  constructor(private http: HttpClient) {}

  getCustomers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.customersUrl}/getcustomers`);
  }

  deleteCustomer(id: number): Observable<any> {
    const url = `${this.customersUrl}/${id}`;
    return this.http.delete<any>(url);
  }
  
  createCustomer(customer: any): Observable<any> {
    return this.http.post<any>(this.customersUrl, customer);
  }
  

  getCustomerById(id: number): Observable<any> {
    const url = `${this.customersUrl}/getcustomerbyid/${id}`;
    return this.http.get<any>(url);
  }

  updateCustomer(customer: any): Observable<any> {
    return this.http.put<any>(this.customersUrl, customer);
  }

  // getCurrencies(): Observable<any[]> {
  //   return this.http.get<any[]>(this.currenciesUrl);
  // }

  getCurrencies(): Observable<any[]> {
    return this.http.get<any[]>(this.currenciesUrl, {
      responseType: 'json'
    }).pipe(
      timeout(50000), 
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse): Observable<any> {
    if (error.error instanceof ErrorEvent) {
      
      console.error('Ocorreu um erro:', error.error.message);
    } else {
      
      console.error(
        `Código de erro ${error.status}, ` +
        `mensagem: ${error.message}`);
    }
    
    return throwError('Ocorreu um erro. Por favor, tente novamente mais tarde.');
  }
}
