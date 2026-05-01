import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

export const roleGuard = (allowedRoles: string[]): CanActivateFn => {
  return (route, state) => {
    const authService = inject(AuthService);
    const router = inject(Router);
    const role = authService.getRole();

    if (role && allowedRoles.includes(role)) {
      return true;
    }

    // Redirect to login if role doesn't match
    authService.logout();
    router.navigate(['/login']);
    return false;
  };
};
