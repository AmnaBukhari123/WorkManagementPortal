// src/Application/DTOs/UserDto.cs
namespace EnterpriseWorkManagementPortal.Application.DTOs;

public record UserDto(int Id, string Email, string FullName, string Role, DateTime CreatedAt);
public record CreateUserDto(string Email, string Password, string FullName);
public record UpdateUserDto(string? FullName, string? Role);