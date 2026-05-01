import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PolicyService } from '../../core/services/policy.service';
import { ClaimService } from '../../core/services/claim.service';
import { AuthService } from '../../core/services/auth.service';
import { PaymentService } from '../../core/services/payment.service';
import { Router } from '@angular/router';
import { NotificationComponent } from '../../shared/components/notification.component';

declare var Razorpay: any;

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, NotificationComponent],
  styleUrls: ['./dashboard.component.css'],
  templateUrl:'./dashboard.component.html'
    
})
export class DashboardComponent implements OnInit {
  activeSection: 'dashboard' | 'tickets' | 'browse' = 'dashboard';
  ticketTab: 'claims' | 'queries' = 'claims';
  userName = '';

  policies: any[] = [];
  claims: any[]   = [];
  queries: any[]  = [];
  allPlans: any[] = [];
  planFilter = '';

  get approvedClaims()   { return this.claims.filter(c => c.status === 'Approved').length; }
  get pendingClaims()    { return this.claims.filter(c => c.status === 'Pending' || c.status === 'Assigned').length; }
  get openQueries()      { return this.queries.filter(q => q.status !== 'Resolved').length; }
  get openTicketsCount() { return this.pendingClaims + this.openQueries; }
  get planTypes(): string[] { return [...new Set<string>(this.allPlans.map(p => p.type))]; }
  get filteredPlans()    { return this.planFilter ? this.allPlans.filter(p => p.type === this.planFilter) : this.allPlans; }

  showClaimForm = false;
  showQueryForm = false;
  submittingClaim = false;
  newClaim: any = { policyId: '', description: '' };
  newQuery: any = { title: '', description: '' };
  selectedClaimFile: File | null = null;
  downloadingPolicyId: number | null = null;

  private policyService  = inject(PolicyService);
  private claimService   = inject(ClaimService);
  private authService    = inject(AuthService);
  private paymentService = inject(PaymentService);
  private router         = inject(Router);

  ngOnInit() { this.userName = this.authService.getName(); this.loadData(); }

  loadData() {
    this.policyService.getMyPolicies().subscribe({ next: r => this.policies = r, error: () => {} });
    this.claimService.getMyClaims().subscribe({ next: r => this.claims = r, error: () => {} });
    this.claimService.getMyQueries().subscribe({ next: r => this.queries = r, error: () => {} });
  }

  loadPlans() {
    if (this.allPlans.length > 0) return;
    this.policyService.getAllPolicies().subscribe({ next: r => this.allPlans = r, error: () => alert('Failed to load plans.') });
  }

  downloadPolicy(id: number) {
    if (!id) { alert('Policy ID missing.'); return; }
    this.downloadingPolicyId = id;
    this.policyService.downloadPolicy(id).subscribe({
      next: blob => {
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url; a.download = `Policy_${id}.pdf`; a.click();
        window.URL.revokeObjectURL(url);
        this.downloadingPolicyId = null;
      },
      error: () => { alert('Failed to download policy.'); this.downloadingPolicyId = null; }
    });
  }

  onFileSelected(event: any) { this.selectedClaimFile = event.target.files[0] ?? null; }

  submitClaim() {
    if (!this.selectedClaimFile) { alert('Please attach an evidence document.'); return; }
    if (!this.newClaim.policyId) { alert('Please select a policy.'); return; }
    this.submittingClaim = true;
    this.claimService.submitClaim({ policyId: this.newClaim.policyId, description: this.newClaim.description }).subscribe({
      next: (res: any) => {
        if (res?.claimId) {
          this.claimService.uploadClaimDocument(res.claimId, this.selectedClaimFile!).subscribe({
            next: () => {
              alert('Claim filed successfully!');
              this.showClaimForm = false;
              this.newClaim = { policyId: '', description: '' };
              this.selectedClaimFile = null;
              this.submittingClaim = false;
              this.loadData();
            },
            error: () => { alert('Claim created, but document upload failed.'); this.submittingClaim = false; this.loadData(); }
          });
        }
      },
      error: (err: any) => { alert(err.error?.message || 'Failed to file claim.'); this.submittingClaim = false; }
    });
  }

  submitQuery() {
    this.claimService.submitQuery(this.newQuery).subscribe({
      next: () => { alert('Query submitted!'); this.showQueryForm = false; this.newQuery = { title: '', description: '' }; this.loadData(); },
      error: () => alert('Failed to submit query.')
    });
  }

  reopenQuery(queryId: number) {
    const comment = prompt('Why are you reopening this query?');
    if (!comment) return;
    this.claimService.reopenQuery({ queryId, additionalComment: comment }).subscribe({
      next: () => { alert('Query reopened.'); this.loadData(); },
      error: () => alert('Failed to reopen query.')
    });
  }

  private getUserId(): string | null {
    const stored = localStorage.getItem('userId');
    if (stored) return stored;
    const token = localStorage.getItem('token');
    if (!token) return null;
    try {
      const decoded = JSON.parse(atob(token.split('.')[1]));
      const id = decoded.sub || decoded.nameid || decoded['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      if (id) localStorage.setItem('userId', id);
      return id ?? null;
    } catch { return null; }
  }

  buyPolicy(policy: any) {
    const userId = this.getUserId();
    if (!userId) { alert('Session expired. Please log in again.'); this.router.navigate(['/login']); return; }
    if (!confirm(`Purchase ${policy.name} for ₹${policy.premium}/mo?`)) return;
    this.paymentService.createOrder(policy.id, policy.premium, userId).subscribe({
      next: order => this.openRazorpay(order, policy.id, userId),
      error: (err: any) => alert(err.error?.message || 'Failed to initiate payment.')
    });
  }

  openRazorpay(order: any, policyId: number, userId: string) {
    const rzp = new Razorpay({
      key: 'rzp_test_SZIIyOVvGBnuzM', amount: order.amount, currency: 'INR',
      name: 'InsurancePortal', description: 'Policy Purchase', order_id: order.id,
      handler: (response: any) => this.verifyPayment(response, policyId, userId),
      prefill: { name: 'Customer', email: 'customer@example.com' },
      theme: { color: '#4F46E5' }
    });
    rzp.open();
  }

  verifyPayment(response: any, policyId: number, userId: string) {
    this.paymentService.verifyPayment({
      razorpayOrderId: response.razorpay_order_id,
      razorpayPaymentId: response.razorpay_payment_id,
      razorpaySignature: response.razorpay_signature,
      userId, policyId
    }).subscribe({
      next: () => { alert('Payment successful! Your policy is now active.'); this.loadData(); this.activeSection = 'dashboard'; },
      error: (err: any) => alert(err.error?.message || err.error?.title || `Verification failed (HTTP ${err.status}).`)
    });
  }

  logout() { this.authService.logout(); this.router.navigate(['/login']); }
}
