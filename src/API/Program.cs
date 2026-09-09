using EnterpriseWorkManagementPortal.API.Middleware;
using EnterpriseWorkManagementPortal.Infrastructure;
using EnterpriseWorkManagementPortal.Application;
using EnterpriseWorkManagementPortal.API.Filters;
using EnterpriseWorkManagementPortal.Application.Validators;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskItemDtoValidator>();
builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>());
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();