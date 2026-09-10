// src/Application/DTOs/DashboardDto.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record RecentProjectDto(int Id, string Title, int TaskCount, bool IsArchived);

public record DashboardSummaryDto(
    int ProjectCount,
    int TaskCount,
    int CompletedTaskCount,
    int TeamMemberCount,
    int PendingTaskCount,
    int InProgressTaskCount,
    List<RecentProjectDto> RecentProjects
);