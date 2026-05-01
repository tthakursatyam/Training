import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NotificationService, Notification } from '../../core/services/notification.service';

@Component({
  selector: 'app-notification',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div style="position: relative; display: inline-block;">
      <button class="btn-secondary" style="border: none; background: transparent; position: relative;" (click)="toggleDropdown()">
        <span style="font-size: 1.5rem;">🔔</span>
        <span *ngIf="unreadCount > 0" class="badge badge-danger" style="position: absolute; top: -5px; right: -5px; font-size: 0.6rem; padding: 0.2rem 0.4rem; border-radius: 50%;">
          {{ unreadCount }}
        </span>
      </button>

      <div *ngIf="isOpen" style="position: absolute; right: 0; top: 100%; width: 300px; background: white; border: 1px solid var(--border); border-radius: var(--radius-md); box-shadow: var(--shadow-lg); z-index: 1000;">
        <div style="padding: 1rem; border-bottom: 1px solid var(--border); display: flex; justify-content: space-between; align-items: center;">
          <h3 class="header-text" style="font-size: 1rem; margin: 0;">Notifications</h3>
          <button class="btn-secondary" style="font-size: 0.75rem; padding: 0.2rem 0.5rem;" (click)="refresh()">Refresh</button>
        </div>
        
        <div style="max-height: 300px; overflow-y: auto;">
          <div *ngIf="notifications.length === 0" style="padding: 1rem; text-align: center;" class="sub-text">
            No notifications.
          </div>
          <div *ngFor="let n of notifications" [ngStyle]="{'background': n.isRead ? 'transparent' : 'var(--background)'}" style="padding: 1rem; border-bottom: 1px solid var(--border); cursor: pointer;" (click)="markAsRead(n.id)">
            <strong style="display: block; font-size: 0.875rem;">{{ n.title }}</strong>
            <span class="sub-text" style="font-size: 0.75rem;">{{ n.message }}</span>
            <div style="font-size: 0.65rem; color: var(--text-muted); margin-top: 0.25rem;">{{ n.createdAt | date:'short' }}</div>
          </div>
        </div>
      </div>
    </div>
  `
})
export class NotificationComponent implements OnInit {
  isOpen = false;
  notifications: Notification[] = [];
  unreadCount = 0;

  private notificationService = inject(NotificationService);

  ngOnInit() {
    this.notificationService.notifications$.subscribe((res: Notification[]) => {
      this.notifications = res;
      this.unreadCount = this.notifications.filter(n => !n.isRead).length;
    });
    this.refresh();
  }

  toggleDropdown() {
    this.isOpen = !this.isOpen;
  }

  refresh() {
    this.notificationService.fetchMyNotifications().subscribe({
      error: () => console.warn('Failed to fetch notifications')
    });
  }

  markAsRead(id: number) {
    const notification = this.notifications.find(n => n.id === id);
    if (notification && !notification.isRead) {
      this.notificationService.markAsRead(id).subscribe();
    }
  }
}
