import { HttpInterceptorFn, HttpRequest, HttpHandlerFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req: HttpRequest<unknown>, next: HttpHandlerFn) => {
  const authService = inject(AuthService);
  const router      = inject(Router);

  // Skip attaching token to the refresh-token call itself to avoid loops
  const isRefreshCall = req.url.includes('/api/auth/refresh-token');

  const token = authService.getToken();
  const authedReq = (token && !isRefreshCall)
    ? req.clone({ headers: req.headers.set('Authorization', `Bearer ${token}`) })
    : req;

  return next(authedReq).pipe(
    catchError((error: HttpErrorResponse) => {
      // Only intercept 401s that are NOT from the refresh call itself
      if (error.status === 401 && !isRefreshCall) {
        return authService.refreshAccessToken().pipe(
          switchMap(newToken => {
            // Retry the original request with the new token
            const retried = req.clone({
              headers: req.headers.set('Authorization', `Bearer ${newToken}`)
            });
            return next(retried);
          }),
          catchError(refreshError => {
            // Refresh failed (token expired/revoked) — force logout
            authService.logout();
            router.navigate(['/login']);
            return throwError(() => refreshError);
          })
        );
      }

      return throwError(() => error);
    })
  );
};
