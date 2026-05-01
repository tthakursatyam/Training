import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  credentials = { email: '', password: '' };
  errorMsg = '';
  showPassword = false;

  private authService = inject(AuthService);
  private router      = inject(Router);

  onSubmit() {
    this.authService.login({ email: this.credentials.email.trim(), password: this.credentials.password.trim() }).subscribe({
      next: () => {
        const role = localStorage.getItem('role');
        const routes: Record<string, string> = { Customer:'/customer', Admin:'/admin', Agent:'/agent', ClaimAdjuster:'/adjuster' };
        this.router.navigate([routes[role ?? ''] ?? '/']);
      },
      error: err => {
        this.errorMsg = err.status === 0
          ? 'Network Error: Cannot reach server (CORS or server is offline).'
          : err.error?.message || 'Invalid email or password.';
      }
    });
  }

  navigateToRegister()       { this.router.navigate(['/register']); }
  navigateToForgotPassword() { this.router.navigate(['/forgot-password']); }
}
