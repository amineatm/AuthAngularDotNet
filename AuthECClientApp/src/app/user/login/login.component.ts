import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../shared/services/auth.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styles: ``,
})
export class LoginComponent {
  isSubmitted: boolean = false;
  constructor(
    public formBuilder: FormBuilder,
    private service: AuthService,
    private toastr: ToastrService,
    private router: Router
  ) {}

  form = this.formBuilder.group({
    email: ['', Validators.required],
    password: ['', Validators.required],
  });

  hasDisplayableErrors(controlName: string): boolean {
    const control = this.form.get(controlName);
    return (
      Boolean(control?.invalid) &&
      (this.isSubmitted || Boolean(control?.touched) || Boolean(control?.dirty))
    );
  }
  onSubmit() {
    this.isSubmitted = true;

    if (this.form.valid) {
      this.service.signin(this.form.value).subscribe({
        next: (res: any) => {
          localStorage.setItem('token', res);
          this.router.navigateByUrl('/dashboard');
        },
        error: (err) => {
          if (err.status == 400) this.toastr.error(err.message, 'Login Failed');
          else this.toastr.error('error during the login.\n');
        },
      });
    }
  }
}
