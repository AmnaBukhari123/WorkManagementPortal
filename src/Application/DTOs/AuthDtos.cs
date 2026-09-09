// src/Application/DTOs/AuthDtos.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record RegisterDto(string Email, string Password, string FullName);
public record LoginDto(string Email, string Password);
public record AuthResponseDto(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt);
public record RefreshRequestDto(string RefreshToken);