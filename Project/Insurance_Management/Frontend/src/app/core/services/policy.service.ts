import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PolicyService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/policy`;

  getAllPolicies() {
    return this.http.get<any[]>(`${this.baseUrl}/all-policies`);
  }

  getMyPolicies() {
    return this.http.get<any[]>(`${this.baseUrl}/my-policies`);
  }

  purchasePolicy(payload: any) {
    return this.http.post(`${this.baseUrl}/purchase`, payload);
  }

  renewPolicy(payload: any) {
    return this.http.post(`${this.baseUrl}/renew`, payload);
  }

  downloadPolicy(policyId: number) {
    return this.http.get(`${this.baseUrl}/download/${policyId}`, { responseType: 'blob' });
  }

  createPolicy(payload: any) {
    return this.http.post(`${this.baseUrl}/create`, payload, { responseType: 'text' });
  }

  updatePolicy(id: number, payload: any) {
    return this.http.put(`${this.baseUrl}/update/${id}`, payload, { responseType: 'text' });
  }

  deletePolicy(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`, { responseType: 'text' });
  }
}
