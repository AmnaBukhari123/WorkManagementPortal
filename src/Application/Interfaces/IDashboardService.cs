// src/Application/Interfaces/IDashboardService.cs
using EnterpriseWorkManagementPortal.Application.DTOs;

namespace EnterpriseWorkManagementPortal.Application.Interfaces;

public interface IDashboardService
{
    Task<DashboardSummaryDto> GetSummaryAsync();
}