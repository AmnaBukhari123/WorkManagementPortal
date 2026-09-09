// src/Application/Interfaces/IAuthService.cs
using EnterpriseWorkManagementPortal.Application.DTOs;

namespace EnterpriseWorkManagementPortal.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
}