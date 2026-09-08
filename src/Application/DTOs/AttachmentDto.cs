// src/Application/DTOs/AttachmentDto.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record AttachmentDto(int Id, string FileName, long FileSizeBytes, string UploadedByName, DateTime UploadedAt);