// src/API/Controllers/CommentsController.cs
using Microsoft.AspNetCore.Mvc;
using EnterpriseWorkManagementPortal.Application.Interfaces;
using EnterpriseWorkManagementPortal.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace EnterpriseWorkManagementPortal.API.Controllers;

[ApiController]
[Route("api")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    public CommentsController(ICommentService commentService) => _commentService = commentService;

    [HttpGet("taskitems/{taskItemId}/comments")]
    public async Task<IActionResult> GetByTask(int taskItemId) => Ok(await _commentService.GetByTaskItemIdAsync(taskItemId));

    [HttpPost("taskitems/{taskItemId}/comments")]
    public async Task<IActionResult> Create(int taskItemId, CreateCommentDto dto)
    {
        if (taskItemId != dto.TaskItemId) return BadRequest("TaskItemId mismatch.");
        return Ok(await _commentService.CreateAsync(dto));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("comments/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _commentService.DeleteAsync(id);
        return NoContent();
    }
}