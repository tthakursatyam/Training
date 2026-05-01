import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ClaimService {
  private http = inject(HttpClient);
  private claimUrl = `${environment.apiUrl}/api/claim`;
  private queryUrl = `${environment.apiUrl}/api/query`;

  submitClaim(payload: any) {
    return this.http.post(this.claimUrl, payload);
  }

  uploadClaimDocument(claimId: number, file: File) {
    const formData = new FormData();
    formData.append('file', file, file.name);
    // Call ClaimService directly to avoid Ocelot multipart buffering issues
    return this.http.post(`http://localhost:5239/api/claim/upload?claimId=${claimId}`, formData);
  }

  submitQuery(payload: any) {
    return this.http.post(`${this.queryUrl}/submit`, payload, { responseType: 'text' });
  }

  getMyQueries() {
    return this.http.get<any[]>(`${this.queryUrl}/my-queries`);
  }

  reopenQuery(payload: any) {
    return this.http.post(`${this.queryUrl}/reopen`, payload, { responseType: 'text' });
  }

  getPendingQueries() {
    return this.http.get<any[]>(`${this.queryUrl}/pending`);
  }

  getMyAssignedQuery() {
    return this.http.get<any>(`${this.queryUrl}/my-assigned-query`);
  }

  resolveQuery(payload: any) {
    return this.http.post(`${this.queryUrl}/resolve`, payload, { responseType: 'text' });
  }

  downloadQueryDocument(queryId: number) {
    return this.http.get(`${this.queryUrl}/document/${queryId}`, { responseType: 'blob' });
  }

  getAssignedClaims() {
    return this.http.get<any[]>(`${this.claimUrl}/my-claims`);
  }

  getAdjusterStats() {
    return this.http.get<any>(`${this.claimUrl}/adjuster-stats`);
  }

  downloadClaimDocument(claimId: number) {
    return this.http.get(`http://localhost:5239/api/claim/document/${claimId}`, { responseType: 'blob', observe: 'response' });
  }

  getMyClaims() {
    return this.http.get<any[]>(`${this.claimUrl}/customer-claims`);
  }

  actionClaim(payload: any) {
    return this.http.post(`${this.claimUrl}/action`, payload, { responseType: 'text' });
  }

  getAllClaims() {
    return this.http.get<any[]>(`${this.claimUrl}/all-claims`);
  }

  getAllQueries() {
    return this.http.get<any[]>(`${this.queryUrl}/all-queries`);
  }

  getAgentStats() {
    return this.http.get<any>(`${environment.apiUrl}/api/agent/stats`);
  }
}
