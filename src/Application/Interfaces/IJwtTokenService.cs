// src/Application/Interfaces/IJwtTokenService.cs
using EnterpriseWorkManagementPortal.Domain.Entities;

namespace EnterpriseWorkManagementPortal.Application.Interfaces;

public interface IJwtTokenService
{
    (string Token, DateTime ExpiresAt) GenerateAccessToken(User user);
    string GenerateRefreshToken();
}