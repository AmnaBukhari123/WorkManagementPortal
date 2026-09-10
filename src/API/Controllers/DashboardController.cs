// src/API/Controllers/DashboardController.cs
using Microsoft.AspNetCore.Mvc;
using EnterpriseWorkManagementPortal.Application.Interfaces;

namespace EnterpriseWorkManagementPortal.API.Controllers;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;
    public DashboardController(IDashboardService dashboardService) => _dashboardService = dashboardService;

    [HttpGet("summary")]
    public async Task<IActionResult> GetSummary() => Ok(await _dashboardService.GetSummaryAsync());
}