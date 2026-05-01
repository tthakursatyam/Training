import { Routes } from '@angular/router';
import { LandingComponent } from './pages/landing/landing.component';
import { LoginComponent } from './pages/auth/login.component';
import { RegisterComponent } from './pages/auth/register.component';
import { ForgotPasswordComponent } from './pages/auth/forgot-password.component';
import { DashboardComponent as CustomerDashboard } from './pages/customer/dashboard.component';
import { MarketplaceComponent } from './pages/customer/marketplace.component';
import { AdminDashboardComponent } from './pages/admin/dashboard.component';
import { AgentDashboardComponent } from './pages/agent/dashboard.component';
import { AdjusterDashboardComponent } from './pages/adjuster/dashboard.component';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role.guard';

export const routes: Routes = [
  { path: '', component: LandingComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'customer', component: CustomerDashboard, canActivate: [authGuard, roleGuard(['Customer'])] },
  { path: 'marketplace', component: MarketplaceComponent, canActivate: [authGuard, roleGuard(['Customer'])] },
  { path: 'admin', component: AdminDashboardComponent, canActivate: [authGuard, roleGuard(['Admin'])] },
  { path: 'agent', component: AgentDashboardComponent, canActivate: [authGuard, roleGuard(['Agent'])] },
  { path: 'adjuster', component: AdjusterDashboardComponent, canActivate: [authGuard, roleGuard(['ClaimAdjuster'])] },
  { path: '**', redirectTo: '' }
];
