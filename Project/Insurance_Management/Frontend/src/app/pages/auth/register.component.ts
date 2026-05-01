import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  userData = { name: '', email: '', password: '' };
  otp = '';
  isOtpStep = false;
  errorMsg = '';
  successMsg = '';

  private authService = inject(AuthService);
  private router      = inject(Router);

  onSubmit() {
    if (!this.userData.name.trim() || !this.userData.email.trim() || !this.userData.password.trim()) {
      this.errorMsg = 'Please fill in all fields.';
      return;
    }
    if (this.userData.password.length < 8) {
      this.errorMsg = 'Password must be at least 8 characters.';
      return;
    }
    this.errorMsg = '';
    this.authService.register(this.userData).subscribe({
      next: () => { this.errorMsg = ''; this.isOtpStep = true; },
      error: err => { this.errorMsg = err.error?.message || 'Failed to register account. Check criteria.'; }
    });
  }

  verifyOtp() {
    this.authService.verifyRegistration({ email: this.userData.email, otp: this.otp }).subscribe({
      next: () => { this.successMsg = 'Account Verified! Redirecting to login...'; this.errorMsg = ''; setTimeout(() => this.router.navigate(['/login']), 2000); },
      error: err => { this.errorMsg = err.error?.message || 'Invalid OTP.'; }
    });
  }

  navigateToLogin() { this.router.navigate(['/login']); }
}
