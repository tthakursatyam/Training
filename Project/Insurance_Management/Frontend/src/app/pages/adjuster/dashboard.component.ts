import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClaimService } from '../../core/services/claim.service';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { NotificationComponent } from '../../shared/components/notification.component';

@Component({
  selector: 'app-adjuster-dashboard',
  standalone: true,
  imports: [CommonModule, NotificationComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class AdjusterDashboardComponent implements OnInit {
  claims: any[] = [];
  stats: any = null;
  downloadingId: number | null = null;
  userName = '';

  private claimService = inject(ClaimService);
  private authService  = inject(AuthService);
  private router       = inject(Router);

  ngOnInit() {
    this.userName = this.authService.getName();
    this.loadClaims();
    this.loadStats();
  }

  loadClaims() {
    this.claimService.getAssignedClaims().subscribe({
      next: res => this.claims = res,
      error: ()  => alert('Failed to load claims.')
    });
  }

  loadStats() {
    this.claimService.getAdjusterStats().subscribe({
      next: res => this.stats = res,
      error: ()  => {}
    });
  }

  actionClaim(id: number, status: string) {
    if (!confirm(`Are you sure you want to ${status.toLowerCase()} this claim?`)) return;
    this.claimService.actionClaim({ claimId: id, status }).subscribe({
      next: () => { this.loadClaims(); this.loadStats(); },
      error: ()  => alert('Failed to update claim.')
    });
  }

  downloadDocument(claimId: number) {
    this.downloadingId = claimId;
    this.claimService.downloadClaimDocument(claimId).subscribe({
      next: response => {
        const blob = response.body!;
        const disposition = response.headers.get('Content-Disposition');
        let fileName = `claim-${claimId}-evidence`;
        if (disposition) {
          const match = disposition.match(/filename[^;=\n]*=((['"]).*?\2|[^;\n]*)/);
          if (match?.[1]) fileName = match[1].replace(/['"]/g, '');
        }
        const url = window.URL.createObjectURL(blob);
        const a = document.createElement('a');
        a.href = url; a.download = fileName; a.click();
        window.URL.revokeObjectURL(url);
        this.downloadingId = null;
      },
      error: err => {
        this.downloadingId = null;
        alert(err.status === 404 ? 'No document was uploaded for this claim.' : 'Failed to download document.');
      }
    });
  }

  logout() { this.authService.logout(); this.router.navigate(['/login']); }
}
