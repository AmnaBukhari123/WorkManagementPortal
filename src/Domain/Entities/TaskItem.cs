// src/Domain/Entities/TaskItem.cs
using EnterpriseWorkManagementPortal.Domain.Enums;

namespace EnterpriseWorkManagementPortal.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public TaskItemStatus Status { get; set; } = TaskItemStatus.Pending;
    public TaskItemPriority Priority { get; set; } = TaskItemPriority.Medium;
    public DateTime? DueDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public int? AssignedToUserId { get; set; }
    public User? AssignedTo { get; set; }

    public List<Comment> Comments { get; set; } = new();
    public List<Attachment> Attachments { get; set; } = new();
}