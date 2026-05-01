import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-forgot-password',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent {
  step = 1;
  email = '';
  otp = '';
  newPassword = '';
  showPassword = false;
  errorMsg = '';
  successMsg = '';
  isLoading = false;

  private authService = inject(AuthService);
  private router      = inject(Router);

  requestOtp() {
    this.isLoading = true; this.errorMsg = '';
    this.authService.forgotPassword({ email: this.email }).subscribe({
      next: () => { this.step = 2; this.isLoading = false; },
      error: err => { this.isLoading = false; this.errorMsg = err.error?.message || 'Failed to send OTP. User might not exist.'; }
    });
  }

  resetPassword() {
    this.isLoading = true; this.errorMsg = '';
    this.authService.resetPassword({ email: this.email, otp: this.otp, newPassword: this.newPassword }).subscribe({
      next: () => { this.successMsg = 'Password reset successfully! Redirecting...'; this.isLoading = false; setTimeout(() => this.router.navigate(['/login']), 2000); },
      error: err => { this.isLoading = false; this.errorMsg = err.error?.message || 'Invalid OTP or password.'; }
    });
  }

  navigateToLogin() { this.router.navigate(['/login']); }
}
