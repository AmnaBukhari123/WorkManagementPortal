// src/Domain/Entities/Project.cs
namespace EnterpriseWorkManagementPortal.Domain.Entities;

public class Project
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsArchived { get; set; }
    public int CreatedByUserId { get; set; }
    public User CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<ProjectMember> Members { get; set; } = new();
    public List<TaskItem> Tasks { get; set; } = new();
}