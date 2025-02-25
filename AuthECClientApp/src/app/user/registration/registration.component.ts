import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidatorFn,
  Validators,
} from '@angular/forms';
import { FirstKeyPipe } from '../../shared/pipes/first-key.pipe';
import { ToastrService } from 'ngx-toastr';
import { Router, RouterLink } from '@angular/router';
import { UserService } from '../../shared/services/user.service';
import { BrowserService } from '../../shared/services/browser.service';
import { AuthService } from '../../shared/services/auth.service';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule, CommonModule, FirstKeyPipe, RouterLink],
  templateUrl: './registration.component.html',
  styles: ``,
})
export class RegistrationComponent implements OnInit {
  roles: string[] = [];
  constructor(
    public formBuilder: FormBuilder,
    public browserService: BrowserService,
    public authService: AuthService,
    private toastr: ToastrService,
    private router: Router,
    private userService: UserService
  ) {}
  ngOnInit(): void {
    if (this.browserService.isLoggedIn()) {
      this.router.navigateByUrl('/dashboard');
    }

    this.userService.getUserRoles().subscribe({
      next: (res: any) => {
        this.roles = res;
      },
      error: (err) => console.error('Failed to fetch roles:', err),
    });
  }
  isSubmitted: boolean = false;

  passwordMatchValidator: ValidatorFn = (control: AbstractControl): null => {
    const password = control.get('password');
    const confirmPassword = control.get('confirmPassword');

    if (password && confirmPassword && password.value != confirmPassword.value)
      confirmPassword?.setErrors({ passwordMismatch: true });
    else confirmPassword?.setErrors(null);

    return null;
  };

  form = this.formBuilder.group(
    {
      fullName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      password: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.pattern(/(?=.*[^a-zA-Z0-9 ])/),
        ],
      ],
      confirmPassword: [''],
      role: ['', Validators.required],
      gender: ['', Validators.required],
      age: ['', [Validators.required, Validators.pattern(/^\d+$/)]],
    },
    { validators: this.passwordMatchValidator }
  );

  onSubmit() {
    this.isSubmitted = true;
    if (this.form.valid) {
      this.authService.createUser(this.form.value).subscribe({
        next: (res: any) => {
          if (res.succeeded) {
            this.form.reset();
            this.isSubmitted = false;
            this.toastr.success(
              'New user was added!',
              'Successful registration!'
            );
            this.router.navigateByUrl('/login');
          }
        },
        error: (err) => {
          if (err.error.errors)
            err.error.errors.forEach((x: any) => {
              switch (x.code) {
                case 'DuplicateUserName':
                  break;

                case 'DuplicateEmail':
                  this.toastr.error(
                    'Email is already taken.',
                    'Registration Failed'
                  );
                  break;

                default:
                  this.toastr.error(
                    'Contact the developer',
                    'Registration Failed'
                  );
                  console.log(x);
                  break;
              }
            });
          else console.log('error:', err);
        },
      });
    }
  }

  hasDisplayableErrors(controlName: string): boolean {
    const control = this.form.get(controlName);
    return (
      Boolean(control?.invalid) &&
      (this.isSubmitted || Boolean(control?.touched) || Boolean(control?.dirty))
    );
  }
}
