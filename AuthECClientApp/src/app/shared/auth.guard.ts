import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from './services/auth.service';
import { BrowserService } from './services/browser.service';

export const authGuard: CanActivateFn = (route, state) => {
  const browserService = inject(BrowserService);
  const router = inject(Router);

  if (browserService.isLoggedIn()) {
    const claimreq = route.data['claimReq'] as Function;
    if (claimreq) {
      const claims = browserService.getClaims();
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
