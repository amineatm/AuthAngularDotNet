import { Injectable } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from './shared/services/auth.service'; // Replace with your auth service path

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService); // Dependency injection
  const router = inject(Router);

  if (authService.isAuthenticated()) {
    return true; // Allow access if authenticated
  }

  router.navigate(['/login']); // Redirect to login if not authenticated
  return false;
};
