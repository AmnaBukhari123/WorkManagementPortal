// tests/EnterpriseWorkManagementPortal.UnitTests/TaskItemServiceTests.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using EnterpriseWorkManagementPortal.Application.DTOs;
using EnterpriseWorkManagementPortal.Application.Services;
using EnterpriseWorkManagementPortal.Infrastructure.Persistence;

namespace EnterpriseWorkManagementPortal.UnitTests;

public class TaskItemServiceTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_AddsTaskItem_ReturnsCorrectDto()
    {
        var context = CreateContext();
        var service = new TaskItemService(context, NullLogger<TaskItemService>.Instance);

        var result = await service.CreateAsync(new CreateTaskItemDto("Test Task", null, "Medium", 1, null));

        Assert.Equal("Test Task", result.Title);
        Assert.Equal("Medium", result.Priority);
        Assert.Single(context.TaskItems);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenTaskDoesNotExist()
    {
        var context = CreateContext();
        var service = new TaskItemService(context, NullLogger<TaskItemService>.Instance);

        var result = await service.GetByIdAsync(999);

        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_ChangesStatus_WhenTaskExists()
    {
        var context = CreateContext();
        var service = new TaskItemService(context, NullLogger<TaskItemService>.Instance);
        var created = await service.CreateAsync(new CreateTaskItemDto("Task", null, "Low", 1, null));

        await service.UpdateAsync(created.Id, new UpdateTaskItemDto(null, null, "Done", null, null));

        var updated = await service.GetByIdAsync(created.Id);
        Assert.Equal("Done", updated!.Status);
    }

    [Fact]
    public async Task UpdateAsync_ThrowsKeyNotFoundException_WhenTaskDoesNotExist()
    {
        var context = CreateContext();
        var service = new TaskItemService(context, NullLogger<TaskItemService>.Instance);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.UpdateAsync(999, new UpdateTaskItemDto("New Title", null, null, null, null)));
    }

    [Fact]
    public async Task DeleteAsync_RemovesTask_WhenTaskExists()
    {
        var context = CreateContext();
        var service = new TaskItemService(context, NullLogger<TaskItemService>.Instance);
        var created = await service.CreateAsync(new CreateTaskItemDto("ToDelete", null, "Low", 1, null));

        await service.DeleteAsync(created.Id);

        Assert.Empty(context.TaskItems);
    }
}