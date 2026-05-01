import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../../environments/environment';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private http    = inject(HttpClient);
  private router  = inject(Router);
  private baseUrl = `${environment.apiUrl}/api/auth`;

  private currentUserSubject = new BehaviorSubject<any>(null);
  public  currentUser$       = this.currentUserSubject.asObservable();

  // Emits true while a token refresh is in progress — interceptor waits on this
  private refreshing = false;
  private refreshSubject = new BehaviorSubject<string | null>(null);

  constructor() { this.initUser(); }

  private initUser() {
    const token = localStorage.getItem('token');
    if (token) {
      this.currentUserSubject.next({
        token,
        userId:   localStorage.getItem('userId'),
        role:     localStorage.getItem('role'),
        name:     localStorage.getItem('userName'),
      });
    }
  }

  // ── Auth actions ──────────────────────────────────────────────────────────

  login(credentials: any) {
    return this.http.post<any>(`${this.baseUrl}/login`, credentials).pipe(
      tap(res => {
        if (res?.accessToken) {
          this.storeSession(res);
        }
      })
    );
  }

  logout() {
    // Revoke refresh token on the server (best-effort)
    const rt = localStorage.getItem('refreshToken');
    if (rt) {
      this.http.post(`${this.baseUrl}/logout`, null, {
        params: { refreshToken: rt },
        responseType: 'text'
      }).subscribe({ error: () => {} });
    }
    this.clearSession();
    this.router.navigate(['/login']);
  }

  register(userData: any)          { return this.http.post(`${this.baseUrl}/register`, userData); }
  verifyRegistration(data: any)    { return this.http.post(`${this.baseUrl}/verify-registration`, data); }
  forgotPassword(data: any)        { return this.http.post(`${this.baseUrl}/forgot-password`, data); }
  resetPassword(data: any)         { return this.http.post(`${this.baseUrl}/reset-password`, data); }

  // ── Token refresh ─────────────────────────────────────────────────────────

  /**
   * Called by the interceptor when it receives a 401.
   * Returns an Observable that emits the new access token on success,
   * or throws so the interceptor can redirect to login.
   */
  refreshAccessToken(): Observable<string> {
    const rt = localStorage.getItem('refreshToken');

    return new Observable<string>(observer => {
      if (!rt) {
        observer.error('No refresh token');
        return;
      }

      if (this.refreshing) {
        // Another request already triggered a refresh — wait for it to finish
        const sub = this.refreshSubject.subscribe(newToken => {
          if (newToken) {
            observer.next(newToken);
            observer.complete();
          }
        });
        return () => sub.unsubscribe();
      }

      this.refreshing = true;
      this.refreshSubject.next(null);

      const sub = this.http.post<any>(
        `${this.baseUrl}/refresh-token`,
        null,
        { params: { refreshToken: rt } }
      ).subscribe({
        next: res => {
          this.storeSession(res);
          this.refreshing = false;
          this.refreshSubject.next(res.accessToken);
          observer.next(res.accessToken);
          observer.complete();
        },
        error: err => {
          this.refreshing = false;
          this.clearSession();
          observer.error(err);
        }
      });

      return () => sub.unsubscribe();
    });
  }

  // ── Helpers ───────────────────────────────────────────────────────────────

  private storeSession(res: any) {
    localStorage.setItem('token',        res.accessToken);
    localStorage.setItem('refreshToken', res.refreshToken);
    localStorage.setItem('role',         res.role   ?? '');
    localStorage.setItem('userId',       res.userId ?? '');
    localStorage.setItem('userName',     res.name   ?? '');
    this.currentUserSubject.next(res);
  }

  private clearSession() {
    ['token', 'refreshToken', 'role', 'userId', 'userName']
      .forEach(k => localStorage.removeItem(k));
    this.currentUserSubject.next(null);
  }

  getToken()  { return localStorage.getItem('token'); }
  getRole()   { return localStorage.getItem('role') ?? ''; }
  getName()   { return localStorage.getItem('userName') ?? ''; }
}
