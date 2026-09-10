// src/Application/Services/DashboardService.cs
using Microsoft.EntityFrameworkCore;
using EnterpriseWorkManagementPortal.Application.DTOs;
using EnterpriseWorkManagementPortal.Application.Interfaces;
using EnterpriseWorkManagementPortal.Domain.Enums;

namespace EnterpriseWorkManagementPortal.Application.Services;

public class DashboardService : IDashboardService
{
    private readonly IApplicationDbContext _context;
    public DashboardService(IApplicationDbContext context) => _context = context;

    public async Task<DashboardSummaryDto> GetSummaryAsync()
    {
        var projectCount = await _context.Projects.CountAsync(p => !p.IsArchived);
        var taskCount = await _context.TaskItems.CountAsync();
        var completedCount = await _context.TaskItems.CountAsync(t => t.Status == TaskItemStatus.Done);
        var pendingCount = await _context.TaskItems.CountAsync(t => t.Status == TaskItemStatus.Pending);
        var inProgressCount = await _context.TaskItems.CountAsync(t => t.Status == TaskItemStatus.InProgress);
        var teamMemberCount = await _context.Users.CountAsync();

        var recentProjects = await _context.Projects
            .Where(p => !p.IsArchived)
            .OrderByDescending(p => p.CreatedAt)
            .Take(5)
            .Select(p => new RecentProjectDto(p.Id, p.Title, p.Tasks.Count, p.IsArchived))
            .AsNoTracking()
            .ToListAsync();

        return new DashboardSummaryDto(
            projectCount, taskCount, completedCount, teamMemberCount,
            pendingCount, inProgressCount, recentProjects);
    }
}