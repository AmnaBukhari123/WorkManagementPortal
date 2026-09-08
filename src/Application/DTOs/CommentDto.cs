// src/Application/DTOs/CommentDto.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record CommentDto(int Id, string Content, string AuthorName, DateTime CreatedAt);
public record CreateCommentDto(string Content, int TaskItemId, int AuthorUserId);