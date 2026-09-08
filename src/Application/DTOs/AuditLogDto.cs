// src/Application/DTOs/AuditLogDto.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record AuditLogDto(int Id, string EntityName, int EntityId, string Action, string ChangedByName, DateTime ChangedAt);