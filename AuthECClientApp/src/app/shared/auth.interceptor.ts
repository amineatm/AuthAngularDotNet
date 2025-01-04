import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { tap } from 'rxjs';
import { environment } from '../../environments/environment';
import { AuthService } from './services/auth.service';
import { BrowserService } from './services/browser.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const browserService = inject(BrowserService);
  const router = inject(Router);
  const toastr = inject(ToastrService);

  let clonedReq = req.clone({
    headers: req.headers.set('Api-Key', environment.ANGULAR_APP_API_KEY),
  });

  if (browserService.isLoggedIn()) {
    clonedReq = clonedReq.clone({
      headers: clonedReq.headers.set(
        'Authorization',
        'Bearer ' + browserService.getToken()
      ),
    });
  }

  return next(clonedReq).pipe(
    tap({
      error: (err: any) => {
        if (err.status == 401) {
          browserService.deleteToken();
          setTimeout(() => {
            toastr.info('Please login again', 'Session Expired!');
          }, 1500);
          router.navigateByUrl('/login');
        } else if (err.status == 403) {
          toastr.error(
            "Oops! It seems you're not authorized to perform the action."
          );
        }
      },
    })
  );
};
