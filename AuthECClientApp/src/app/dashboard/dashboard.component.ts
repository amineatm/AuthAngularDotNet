import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/services/user.service';
import { HideIfClaimsNotMetDirective } from '../directives/hide-if-claims-not-met.directive';
import { claimReq } from '../shared/utils/claimReq-utils';

@Component({
  selector: 'app-dashboard',
  imports: [HideIfClaimsNotMetDirective],
  templateUrl: './dashboard.component.html',
  styles: ``,
})
export class DashboardComponent implements OnInit {
  constructor(private userService: UserService) {}
  claimReq = claimReq;
  fullName: string = '';
  email: string = '';

  ngOnInit(): void {
    this.userService.getUserProfile().subscribe({
      next: (res: any) => {
        this.fullName = res.fullName;
        this.email = res.email;
      },
      error: (err: any) =>
        console.log('error while retrieving user profile data'),
    });
  }
}
