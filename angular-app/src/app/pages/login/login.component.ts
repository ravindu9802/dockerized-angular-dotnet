import { Component, inject } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { AuthService } from '../../core/services/auth.service';
import { HttpResponse } from '@angular/common/http';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  private authService = inject(AuthService);
  private router = inject(Router);
  loginForm: FormGroup;

  constructor() {
    this.loginForm = new FormGroup({
      email: new FormControl('', [Validators.required, Validators.email]),
      password: new FormControl('', [
        Validators.required,
        Validators.minLength(5),
      ]),
    });
  }

  ngOnInit() {
    localStorage.clear();
  }

  onSubmit() {
    const isValid = this.loginForm.valid;
    const values = this.loginForm.value;

    if (isValid) {
      this.authService.validateLogin(values.email, values.password).subscribe({
        next: (res: HttpResponse<any>) => {
          if (res.status == 200) {
            this.router.navigateByUrl('/home');
            localStorage.setItem('isLogged', 'TRUE');
          }
        },
        error: (e) => {
          console.error(e);
          alert('Invalid login credentials');
        },
        complete: () => console.info('validateLogin complete'),
      });
    }
  }
}
