import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-registration',
  imports: [ReactiveFormsModule],
  templateUrl: './registration.component.html',
  styles: ``,
})
export class RegistrationComponent implements OnInit {
  form!: FormGroup;

  constructor(private formBuilder: FormBuilder) {}

  ngOnInit(): void {
    this.form = this.formBuilder.group({
      fullname: [''],
      email: [''],
      password: [''],
      confirmPassword: [''],
    });
  }

  onSubmit() {
    console.log(this.form.value);
  }
}
