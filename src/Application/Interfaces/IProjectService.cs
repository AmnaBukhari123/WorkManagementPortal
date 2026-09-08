// src/Application/Interfaces/IProjectService.cs
using EnterpriseWorkManagementPortal.Application.DTOs;
using EnterpriseWorkManagementPortal.Application.Common;
namespace EnterpriseWorkManagementPortal.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto?> GetByIdAsync(int id);
    Task<PagedResult<ProjectDto>> GetAllAsync(ProjectQueryParams query);
    Task<ProjectDto> CreateAsync(CreateProjectDto dto);
    Task UpdateAsync(int id, UpdateProjectDto dto);
    Task DeleteAsync(int id);
    Task<List<ProjectMemberDto>> GetMembersAsync(int projectId);
    Task AddMemberAsync(int projectId, int userId);
    Task RemoveMemberAsync(int projectId, int userId);
}