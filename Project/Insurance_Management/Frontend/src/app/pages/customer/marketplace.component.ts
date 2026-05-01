import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PolicyService } from '../../core/services/policy.service';
import { PaymentService } from '../../core/services/payment.service';
import { Router } from '@angular/router';

declare var Razorpay: any;

@Component({
  selector: 'app-marketplace',
  standalone: true,
  imports: [CommonModule],
  templateUrl: `./marketplace.component.html`
})
export class MarketplaceComponent implements OnInit {
  policies: any[] = [];
  
  private policyService = inject(PolicyService);
  private paymentService = inject(PaymentService);
  private router = inject(Router);

  ngOnInit() {
    this.policyService.getAllPolicies().subscribe({
      next: (res) => this.policies = res,
      error: () => alert('Failed to load marketplace.')
    });
  }

  private getUserId(): string | null {
    // First try localStorage
    const storedId = localStorage.getItem('userId');
    if (storedId) return storedId;

    // Fallback: decode from JWT token
    const token = localStorage.getItem('token');
    if (!token) return null;
    try {
      const payload = token.split('.')[1];
      const decoded = JSON.parse(atob(payload));
      // JWT uses 'sub' claim (JwtRegisteredClaimNames.Sub) for user ID
      const userId = decoded.sub || decoded.nameid || decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      if (userId) {
        localStorage.setItem('userId', userId); // Cache it for future use
      }
      return userId || null;
    } catch (e) {
      return null;
    }
  }

  buyPolicy(policy: any) {
    const userId = this.getUserId();
    if (!userId) {
      alert("Session expired. Please log in again.");
      this.router.navigate(['/login']);
      return;
    }

    if(confirm(`Confirm purchase of ${policy.name} for $${policy.premium}?`)) {
      this.paymentService.createOrder(policy.id, policy.premium, userId).subscribe({
        next: (order) => {
          this.openRazorpay(order, policy.id, userId);
        },
        error: (err) => alert(err.error?.message || "Failed to initiate payment. Please try again.")
      });
    }
  }

  openRazorpay(order: any, policyId: number, userId: string) {
    const options = {
      key: 'rzp_test_SZIIyOVvGBnuzM',
      amount: order.amount,
      currency: 'INR',
      name: 'Insurance Management',
      description: 'Policy Purchase',
      order_id: order.id,
      handler: (response: any) => {
        this.verifyPayment(response, policyId, userId);
      },
      prefill: {
        name: 'Customer',
        email: 'customer@example.com'
      },
      theme: {
        color: '#2563eb'
      }
    };

    const rzp = new Razorpay(options);
    rzp.open();
  }

  verifyPayment(response: any, policyId: number, userId: string) {
    const payload = {
      razorpayOrderId: response.razorpay_order_id,
      razorpayPaymentId: response.razorpay_payment_id,
      razorpaySignature: response.razorpay_signature,
      userId: userId,
      policyId: policyId
    };

    console.log('Sending verify payload:', payload);

    this.paymentService.verifyPayment(payload).subscribe({
      next: () => {
        alert("Payment Successful! Your policy is now active.");
        this.router.navigate(['/customer']);
      },
      error: (err) => {
        console.error('Payment verification error:', err);
        const message = err.error?.message
          || err.error?.title
          || (err.error?.errors ? JSON.stringify(err.error.errors) : null)
          || `Payment verification failed (HTTP ${err.status}). Please contact support.`;
        alert(message);
      }
    });
  }

  goBack() {
    this.router.navigate(['/customer']);
  }
}
