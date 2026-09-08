// src/Application/DTOs/ProjectDto.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record ProjectDto(int Id, string Title, string? Description, bool IsArchived, string CreatedByName, DateTime CreatedAt);
public record CreateProjectDto(string Title, string? Description, int CreatedByUserId);
public record UpdateProjectDto(string? Title, string? Description, bool? IsArchived);
public record ProjectMemberDto(int UserId, string FullName, string Email);
public record ProjectQueryParams(string? Search, bool? IsArchived, int Page = 1, int PageSize = 20);