// src/Web/src/app/features/dashboard/dashboard.component.ts
import { Component, inject, signal, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RouterLink } from '@angular/router';
import { DashboardService } from '../../shared/services/dashboard.service';
import { DashboardSummary } from '../../shared/models/dashboard.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  private dashboardService = inject(DashboardService);
  private router = inject(Router);

  summary = signal<DashboardSummary | null>(null);
  loading = signal(true);

  ngOnInit(): void {
    this.dashboardService.getSummary().subscribe({
      next: data => {
        this.summary.set(data);
        this.loading.set(false);
      },
      error: () => this.loading.set(false)
    });
  }

  taskStatusPercent(count: number): number {
    const total = this.summary()?.taskCount || 1;
    return Math.round((count / total) * 100);
  }

  goToNewProject(): void {
    this.router.navigate(['/projects/new']);
  }

  initials(title: string): string {
    return title.split(' ').map(w => w[0]).join('').slice(0, 2).toUpperCase();
  }
}