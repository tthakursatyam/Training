import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  private http = inject(HttpClient);
  private baseUrl = `${environment.apiUrl}/api/admin`;

  getAgents() {
    return this.http.get<any[]>(`${this.baseUrl}/agents`);
  }

  getUsers() {
    return this.http.get<any[]>(`${this.baseUrl}/users`);
  }

  getClaimAdjusters() {
    return this.http.get<any[]>(`${this.baseUrl}/claim-adjusters`);
  }

  createAgent(payload: any) {
    return this.http.post(`${this.baseUrl}/create-agent`, payload, { responseType: 'text' });
  }

  createClaimAdjuster(payload: any) {
    return this.http.post(`${this.baseUrl}/create-claim-adjuster`, payload, { responseType: 'text' });
  }

  toggleUser(id: string) {
    return this.http.put(`${this.baseUrl}/toggle-user/${id}`, {}, { responseType: 'text' });
  }

  // Reuses the same toggle-user endpoint — works for any role (Agent, ClaimAdjuster)
  toggleStaff(id: string) {
    return this.http.put(`${this.baseUrl}/toggle-user/${id}`, {}, { responseType: 'text' });
  }

  getUserStats() {
    return this.http.get<any>(`${this.baseUrl}/stats/users`);
  }

  getPolicyStats() {
    return this.http.get<any>(`${this.baseUrl}/stats/policies`);
  }

  getClaimStats() {
    return this.http.get<any>(`${this.baseUrl}/stats/claims`);
  }
}
