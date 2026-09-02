// src/Domain/Entities/Comment.cs
namespace EnterpriseWorkManagementPortal.Domain.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; }
    public int AuthorUserId { get; set; }
    public User Author { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}