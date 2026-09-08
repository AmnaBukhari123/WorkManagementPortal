// src/Application/Services/AttachmentService.cs
using Microsoft.EntityFrameworkCore;
using EnterpriseWorkManagementPortal.Application.DTOs;
using EnterpriseWorkManagementPortal.Application.Interfaces;
using EnterpriseWorkManagementPortal.Domain.Entities;

namespace EnterpriseWorkManagementPortal.Application.Services;

public class AttachmentService : IAttachmentService
{
    private readonly IApplicationDbContext _context;
    private readonly IFileStorageService _fileStorage;

    public AttachmentService(IApplicationDbContext context, IFileStorageService fileStorage)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<List<AttachmentDto>> GetByTaskItemIdAsync(int taskItemId) =>
        (await _context.Attachments.Include(a => a.UploadedBy)
            .Where(a => a.TaskItemId == taskItemId).AsNoTracking().ToListAsync())
            .Select(Map).ToList();

    public async Task<AttachmentDto> UploadAsync(int taskItemId, Stream fileStream, string fileName, int uploadedByUserId)
    {
        var (filePath, fileSizeBytes) = await _fileStorage.SaveFileAsync(fileStream, fileName);

        var attachment = new Attachment
        {
            TaskItemId = taskItemId,
            FileName = fileName,
            FilePath = filePath,
            FileSizeBytes = fileSizeBytes,
            UploadedByUserId = uploadedByUserId
        };
        _context.Attachments.Add(attachment);
        await _context.SaveChangesAsync();
        await _context.Entry(attachment).Reference(a => a.UploadedBy).LoadAsync();
        return Map(attachment);
    }

    public async Task DeleteAsync(int id)
    {
        var attachment = await _context.Attachments.FindAsync(id)
            ?? throw new KeyNotFoundException($"Attachment {id} not found.");

        await _fileStorage.DeleteFileAsync(attachment.FilePath);
        _context.Attachments.Remove(attachment);
        await _context.SaveChangesAsync();
    }

    private static AttachmentDto Map(Attachment a) => new(a.Id, a.FileName, a.FileSizeBytes, a.UploadedBy?.FullName ?? "", a.UploadedAt);
}