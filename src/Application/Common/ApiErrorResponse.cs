// src/Application/Common/ApiErrorResponse.cs
namespace EnterpriseWorkManagementPortal.Application.Common;

public record ApiErrorResponse(int StatusCode, string Message, List<string>? Errors = null);