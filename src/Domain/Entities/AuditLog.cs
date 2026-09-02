// src/Domain/Entities/AuditLog.cs
namespace EnterpriseWorkManagementPortal.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }
    public string EntityName { get; set; }
    public int EntityId { get; set; }
    public string Action { get; set; }
    public string? ChangedFieldsJson { get; set; }
    public int ChangedByUserId { get; set; }
    public User ChangedBy { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}