// src/API/Controllers/AuthController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EnterpriseWorkManagementPortal.Application.Interfaces;
using EnterpriseWorkManagementPortal.Application.DTOs;
using Microsoft.AspNetCore.RateLimiting;

namespace EnterpriseWorkManagementPortal.API.Controllers;

[ApiController]
[Route("api/auth")]
[AllowAnonymous]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto) => Ok(await _authService.RegisterAsync(dto));

    [HttpPost("login")]
    [EnableRateLimiting("login")]
    public async Task<IActionResult> Login(LoginDto dto) => Ok(await _authService.LoginAsync(dto));

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequestDto dto) => Ok(await _authService.RefreshTokenAsync(dto.RefreshToken));
}