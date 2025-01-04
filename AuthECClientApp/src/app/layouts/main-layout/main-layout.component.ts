import { Component } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { HideIfClaimsNotMetDirective } from '../../directives/hide-if-claims-not-met.directive';
import { claimReq } from '../../shared/utils/claimReq-utils';
import { BrowserService } from '../../shared/services/browser.service';

@Component({
  selector: 'app-main-layout',
  imports: [RouterOutlet, RouterLink, HideIfClaimsNotMetDirective],
  templateUrl: './main-layout.component.html',
  styles: ``,
})
export class MainLayoutComponent {
  constructor(private router: Router, private browserService: BrowserService) { }
  claimReq = claimReq;

  onLogout() {
    this.browserService.deleteToken();
    this.router.navigateByUrl('/login');
  }
}
