import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/payment`;

  createOrder(policyId: number, amount: number, userId: string) {
    return this.http.post<any>(`${this.baseUrl}/create-order`, { policyId, amount, userId });
  }

  verifyPayment(paymentData: any) {
    return this.http.post(`${this.baseUrl}/verify`, paymentData);
  }
}
