// src/Application/DTOs/TaskItemDto.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record TaskItemDto(
    int Id, string Title, string? Description, string Status, string Priority,
    DateTime? DueDate, int ProjectId, string? AssignedToName);

public record CreateTaskItemDto(string Title, string? Description, string Priority, int ProjectId, int? AssignedToUserId);
public record UpdateTaskItemDto(string? Title, string? Description, string? Status, string? Priority, DateTime? DueDate);
public record TaskItemQueryParams(string? Status, string? Priority, string? Search, int Page = 1, int PageSize = 20);