// src/Domain/Entities/User.cs
using EnterpriseWorkManagementPortal.Domain.Enums;

namespace EnterpriseWorkManagementPortal.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string FullName { get; set; }
    public UserRole Role { get; set; } = UserRole.Member;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<ProjectMember> ProjectMemberships { get; set; } = new();
    public List<TaskItem> AssignedTasks { get; set; } = new();
}