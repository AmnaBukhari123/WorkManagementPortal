// src/API/Controllers/AuditLogsController.cs
using Microsoft.AspNetCore.Mvc;
using EnterpriseWorkManagementPortal.Application.Interfaces;

namespace EnterpriseWorkManagementPortal.API.Controllers;

[ApiController]
[Route("api/audit-logs")]
public class AuditLogsController : ControllerBase
{
    private readonly IAuditLogService _auditLogService;
    public AuditLogsController(IAuditLogService auditLogService) => _auditLogService = auditLogService;

    [HttpGet]
    public async Task<IActionResult> GetByEntity([FromQuery] string entityName, [FromQuery] int entityId) =>
        Ok(await _auditLogService.GetByEntityAsync(entityName, entityId));
}