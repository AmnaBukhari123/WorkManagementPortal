// src/Application/Interfaces/ICommentService.cs
using EnterpriseWorkManagementPortal.Application.DTOs;

namespace EnterpriseWorkManagementPortal.Application.Interfaces;

public interface ICommentService
{
    Task<List<CommentDto>> GetByTaskItemIdAsync(int taskItemId);
    Task<CommentDto> CreateAsync(CreateCommentDto dto);
    Task DeleteAsync(int id);
}