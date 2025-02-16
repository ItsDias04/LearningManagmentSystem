import { inject } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';

export const canActivateAuth = (allowedRoles: string[]) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isAuth && authService.roleAccess(allowedRoles)) {
    return true;
  }
  return router.createUrlTree(['/login']);
};
