import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ClaimService } from '../../core/services/claim.service';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { NotificationComponent } from '../../shared/components/notification.component';

@Component({
  selector: 'app-agent-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, NotificationComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class AgentDashboardComponent implements OnInit {
  assignedQuery: any = null;
  responseText = '';
  queueCount = 0;
  isLoading = true;
  isResolving = false;
  stats: any = null;
  userName = '';

  private claimService = inject(ClaimService);
  private authService  = inject(AuthService);
  private router       = inject(Router);

  ngOnInit() {
    this.userName = this.authService.getName();
    this.loadAssignment();
    this.loadQueueCount();
    this.loadStats();
  }

  loadStats() { this.claimService.getAgentStats().subscribe(res => this.stats = res); }

  loadAssignment() {
    this.isLoading = true;
    this.claimService.getMyAssignedQuery().subscribe({
      next: res => { this.assignedQuery = res; this.responseText = ''; this.isLoading = false; },
      error: ()  => { this.assignedQuery = null; this.isLoading = false; }
    });
  }

  loadQueueCount() {
    this.claimService.getPendingQueries().subscribe({
      next: res => this.queueCount = res?.length ?? 0,
      error: ()  => this.queueCount = 0
    });
  }

  resolveQuery() {
    if (!this.responseText.trim()) { alert('Please type a resolution before closing the ticket.'); return; }
    this.isResolving = true;
    this.claimService.resolveQuery({ queryId: this.assignedQuery.id, response: this.responseText }).subscribe({
      next: () => { this.isResolving = false; setTimeout(() => { this.loadAssignment(); this.loadQueueCount(); }, 500); },
      error: ()  => { this.isResolving = false; alert('Failed to resolve the ticket. Please try again.'); }
    });
  }

  downloadAttachment(id: number) {
    this.claimService.downloadQueryDocument(id).subscribe({
      next: blob => { const url = window.URL.createObjectURL(blob); window.open(url); },
      error: ()  => alert('No attachment found for this query.')
    });
  }

  logout() { this.authService.logout(); this.router.navigate(['/login']); }
}
