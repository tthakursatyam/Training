import { Component, inject, HostListener } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-landing',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.css']
})
export class LandingComponent {
  scrolled = false;
  private router = inject(Router);

  @HostListener('window:scroll')
  onScroll() { this.scrolled = window.scrollY > 40; }

  goLogin()    { this.router.navigate(['/login']); }
  goRegister() { this.router.navigate(['/register']); }

  scrollTo(id: string) {
    document.getElementById(id)?.scrollIntoView({ behavior: 'smooth', block: 'start' });
  }
}
