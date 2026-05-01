import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AdminService } from '../../core/services/admin.service';
import { PolicyService } from '../../core/services/policy.service';
import { ClaimService } from '../../core/services/claim.service';
import { AuthService } from '../../core/services/auth.service';
import { Router } from '@angular/router';
import { NotificationComponent } from '../../shared/components/notification.component';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, FormsModule, NotificationComponent],
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  tab = 'overview';
  ticketTab = 'claims';
  userName = '';
  today = new Date();

  tabTitles: Record<string,string> = {
    overview:'Overview', users:'Customers', staff:'Staff Management',
    policies:'Policy Catalog', tickets:'Tickets', analytics:'Analytics'
  };
  tabSubs: Record<string,string> = {
    overview:'Your system at a glance',
    users:'Manage customer accounts',
    staff:'Agents and claim adjusters',
    policies:'Insurance products catalog',
    tickets:'Claims and support queries',
    analytics:'Performance metrics'
  };

  users: any[] = [];
  agents: any[] = [];
  adjusters: any[] = [];
  allPolicies: any[] = [];
  allClaims: any[] = [];
  allQueries: any[] = [];
  userStats: any = null;
  policyStats: any = null;
  claimStats: any = null;
  selectedCustomer: any = null;

  userSearch = '';
  policySearch = '';
  claimSearch = '';
  querySearch = '';

  newStaff = { role: 'Agent', name: '', email: '', password: '' };
  newPolicy = { name: '', type: '', premium: 0, coverage: '', terms: '' };
  editingPolicy: any = null;   // holds the policy being edited (null = create mode)

  get filteredUsers()    { const s=this.userSearch.toLowerCase(); return s ? this.users.filter(u=>u.name?.toLowerCase().includes(s)||u.email?.toLowerCase().includes(s)) : this.users; }
  get filteredPolicies() { const s=this.policySearch.toLowerCase(); return s ? this.allPolicies.filter(p=>p.name?.toLowerCase().includes(s)||p.type?.toLowerCase().includes(s)) : this.allPolicies; }
  get filteredClaims()   { const s=this.claimSearch.toLowerCase(); return s ? this.allClaims.filter(c=>String(c.id).includes(s)||c.description?.toLowerCase().includes(s)||c.status?.toLowerCase().includes(s)) : this.allClaims; }
  get filteredQueries()  { const s=this.querySearch.toLowerCase(); return s ? this.allQueries.filter(q=>String(q.id).includes(s)||q.title?.toLowerCase().includes(s)||q.status?.toLowerCase().includes(s)) : this.allQueries; }
  get pendingTickets()   { return this.allClaims.filter(c=>c.status==='Pending'||c.status==='Assigned').length + this.allQueries.filter(q=>q.status!=='Resolved').length; }

  private adminService  = inject(AdminService);
  private policyService = inject(PolicyService);
  private claimService  = inject(ClaimService);
  private authService   = inject(AuthService);
  private router        = inject(Router);

  ngOnInit() {
    this.userName = this.authService.getName();
    this.loadUsers(); this.loadStaff(); this.loadPolicies(); this.loadTickets(); this.loadAnalytics();
  }

  loadAnalytics() {
    this.adminService.getUserStats().subscribe(r => this.userStats = r);
    this.adminService.getPolicyStats().subscribe(r => this.policyStats = r);
    this.adminService.getClaimStats().subscribe(r => this.claimStats = r);
  }
  loadUsers()    { this.adminService.getUsers().subscribe(r => this.users = r); }
  loadStaff()    { this.adminService.getAgents().subscribe(r => this.agents = r); this.adminService.getClaimAdjusters().subscribe(r => this.adjusters = r); }
  loadPolicies() { this.policyService.getAllPolicies().subscribe(r => this.allPolicies = r); }
  loadTickets()  {
    this.claimService.getAllClaims().subscribe({ next: r => this.allClaims = r, error: () => {} });
    this.claimService.getAllQueries().subscribe({ next: r => this.allQueries = r, error: () => {} });
  }

  toggleUser(id: string) { this.adminService.toggleUser(id).subscribe(() => this.loadUsers()); }

  createStaff() {
    const obs = this.newStaff.role === 'Agent'
      ? this.adminService.createAgent(this.newStaff)
      : this.adminService.createClaimAdjuster(this.newStaff);
    obs.subscribe({ next: () => { this.loadStaff(); this.newStaff = { role:'Agent', name:'', email:'', password:'' }; }, error: () => alert('Failed to create staff.') });
  }

  createPolicy() {
    this.policyService.createPolicy(this.newPolicy).subscribe({
      next: () => { alert('Policy published!'); this.newPolicy = { name:'', type:'', premium:0, coverage:'', terms:'' }; this.loadPolicies(); },
      error: () => alert('Failed to create policy.')
    });
  }

  startEditPolicy(policy: any) {
    this.editingPolicy = { ...policy };   // clone so cancel works cleanly
  }

  cancelEditPolicy() {
    this.editingPolicy = null;
  }

  saveEditPolicy() {
    if (!this.editingPolicy) return;
    this.policyService.updatePolicy(this.editingPolicy.id, {
      name:     this.editingPolicy.name,
      type:     this.editingPolicy.type,
      premium:  this.editingPolicy.premium,
      coverage: this.editingPolicy.coverage,
      terms:    this.editingPolicy.terms
    }).subscribe({
      next: () => { alert('Policy updated!'); this.editingPolicy = null; this.loadPolicies(); },
      error: () => alert('Failed to update policy.')
    });
  }

  confirmDeletePolicy(policy: any) {
    if (!confirm(`Delete "${policy.name}"? This cannot be undone.`)) return;
    this.policyService.deletePolicy(policy.id).subscribe({
      next: () => { this.loadPolicies(); },
      error: () => alert('Failed to delete policy.')
    });
  }

  toggleStaff(id: string) {
    this.adminService.toggleStaff(id).subscribe({
      next: () => this.loadStaff(),
      error: () => alert('Failed to update staff status.')
    });
  }

  getPercentage(part: number|undefined, total: number|undefined): number {
    if (!total) return 0;
    return Math.min(100, ((part ?? 0) / total) * 100);
  }

  statusTag(status: string): string {
    const map: Record<string,string> = { Approved:'tag-green', Resolved:'tag-green', Rejected:'tag-red', Pending:'tag-yellow', Assigned:'tag-blue', Reopened:'tag-yellow' };
    return map[status] ?? 'tag-gray';
  }

  avatarColor(name: string): string {
    const colors = ['#6366F1','#8B5CF6','#EC4899','#F59E0B','#10B981','#3B82F6','#EF4444','#14B8A6'];
    return colors[(name?.charCodeAt(0) ?? 0) % colors.length];
  }

  getAdjusterName(id: string|null): string { if (!id) return '—'; const f=this.adjusters.find((a:any)=>a.id===id); return f?f.name:'—'; }
  getAgentName(id: string|null): string    { if (!id) return '—'; const f=this.agents.find((a:any)=>a.id===id); return f?f.name:'—'; }
  getCustomerName(id: string|null): string { if (!id) return 'Unknown'; const f=this.users.find((u:any)=>u.id===id); return f?f.name:'Unknown'; }

  showCustomerPopup(userId: string, ticketDate: string) {
    const f = this.users.find((u:any) => u.id === userId);
    this.selectedCustomer = f ? { name:f.name, email:f.email, ticketDate } : { name:'Unknown', email:'N/A', ticketDate };
  }

  logout() { this.authService.logout(); this.router.navigate(['/login']); }
}
