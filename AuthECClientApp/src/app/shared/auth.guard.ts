import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './services/auth.service';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  if (authService.isLoggedIn()) {
    const claimreq = route.data['claimReq'] as Function;
    if (claimreq) {
      const claims = authService.getClaims();
      if (!claimreq(claims)) {
        router.navigateByUrl('/forbidden');
        return false;
      }
      return true;
    }
    return true;
  } else {
    router.navigateByUrl('/login');
    return false;
  }
};
