// src/Web/src/app/shared/models/dashboard.model.ts
export interface RecentProject {
  id: number;
  title: string;
  taskCount: number;
  isArchived: boolean;
}

export interface DashboardSummary {
  projectCount: number;
  taskCount: number;
  completedTaskCount: number;
  teamMemberCount: number;
  pendingTaskCount: number;
  inProgressTaskCount: number;
  recentProjects: RecentProject[];
}