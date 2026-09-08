// src/Infrastructure/DependencyInjection.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnterpriseWorkManagementPortal.Application.Interfaces;
using EnterpriseWorkManagementPortal.Infrastructure.Persistence;
using EnterpriseWorkManagementPortal.Infrastructure.FileStorage;

namespace EnterpriseWorkManagementPortal.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddSingleton<IFileStorageService>(_ =>
             new LocalFileStorageService(Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles")));

        return services;
    }
}