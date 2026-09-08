// src/API/Controllers/UsersController.cs
using Microsoft.AspNetCore.Mvc;
using EnterpriseWorkManagementPortal.Application.Interfaces;
using EnterpriseWorkManagementPortal.Application.DTOs;

namespace EnterpriseWorkManagementPortal.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _userService.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto) => Ok(await _userService.CreateAsync(dto));

    [HttpPatch("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        await _userService.UpdateAsync(id, dto);
        return NoContent();
    }
}