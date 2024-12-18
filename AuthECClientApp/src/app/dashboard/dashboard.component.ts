import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { TOKEN_KEY } from '../shared/constants';
import { AuthService } from '../shared/services/auth.service';

@Component({
  selector: 'app-dashboard',
  imports: [],
  templateUrl: './dashboard.component.html',
  styles: ``,
})
export class DashboardComponent {
  constructor(private router: Router, private authService: AuthService) {}

  onLogout() {
    this.authService.deleteToken();
    this.router.navigateByUrl('/login');
  }
}
