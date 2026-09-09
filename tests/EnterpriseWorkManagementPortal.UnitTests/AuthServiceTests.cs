// tests/EnterpriseWorkManagementPortal.UnitTests/AuthServiceTests.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit;
using EnterpriseWorkManagementPortal.Application.DTOs;
using EnterpriseWorkManagementPortal.Application.Services;
using EnterpriseWorkManagementPortal.Infrastructure.Persistence;
using EnterpriseWorkManagementPortal.Infrastructure.Authentication;
using Microsoft.Extensions.Options;

namespace EnterpriseWorkManagementPortal.UnitTests;

public class AuthServiceTests
{
    private static AppDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }

    private static JwtTokenService CreateTokenService()
    {
        var settings = Options.Create(new JwtSettings
        {
            Key = "test-signing-key-at-least-32-characters-long",
            Issuer = "TestIssuer",
            Audience = "TestAudience",
            AccessTokenExpirationMinutes = 15
        });
        return new JwtTokenService(settings);
    }

    [Fact]
    public async Task RegisterAsync_CreatesUser_ReturnsTokens()
    {
        var context = CreateContext();
        var service = new AuthService(context, CreateTokenService(), NullLogger<AuthService>.Instance);

        var result = await service.RegisterAsync(new RegisterDto("test@test.com", "password123", "Test User"));

        Assert.NotNull(result.AccessToken);
        Assert.NotNull(result.RefreshToken);
        Assert.Single(context.Users);
    }

    [Fact]
    public async Task RegisterAsync_ThrowsArgumentException_WhenEmailAlreadyExists()
    {
        var context = CreateContext();
        var service = new AuthService(context, CreateTokenService(), NullLogger<AuthService>.Instance);
        await service.RegisterAsync(new RegisterDto("dup@test.com", "password123", "First"));

        await Assert.ThrowsAsync<ArgumentException>(() =>
            service.RegisterAsync(new RegisterDto("dup@test.com", "password456", "Second")));
    }

    [Fact]
    public async Task LoginAsync_ThrowsUnauthorized_WhenPasswordIsWrong()
    {
        var context = CreateContext();
        var service = new AuthService(context, CreateTokenService(), NullLogger<AuthService>.Instance);
        await service.RegisterAsync(new RegisterDto("user@test.com", "correctpassword", "User"));

        await Assert.ThrowsAsync<UnauthorizedAccessException>(() =>
            service.LoginAsync(new LoginDto("user@test.com", "wrongpassword")));
    }

    [Fact]
    public async Task LoginAsync_Succeeds_WithCorrectCredentials()
    {
        var context = CreateContext();
        var service = new AuthService(context, CreateTokenService(), NullLogger<AuthService>.Instance);
        await service.RegisterAsync(new RegisterDto("user2@test.com", "mypassword", "User Two"));

        var result = await service.LoginAsync(new LoginDto("user2@test.com", "mypassword"));

        Assert.NotNull(result.AccessToken);
    }
}