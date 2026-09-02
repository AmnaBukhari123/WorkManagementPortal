// src/Domain/Entities/Attachment.cs
namespace EnterpriseWorkManagementPortal.Domain.Entities;

public class Attachment
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSizeBytes { get; set; }
    public int TaskItemId { get; set; }
    public TaskItem TaskItem { get; set; }
    public int UploadedByUserId { get; set; }
    public User UploadedBy { get; set; }
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
}