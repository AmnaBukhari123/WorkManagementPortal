// src/Application/Validators/TaskItemValidators.cs
using FluentValidation;
using EnterpriseWorkManagementPortal.Application.DTOs;
using EnterpriseWorkManagementPortal.Domain.Enums;

namespace EnterpriseWorkManagementPortal.Application.Validators;

public class CreateTaskItemDtoValidator : AbstractValidator<CreateTaskItemDto>
{
    public CreateTaskItemDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Description).MaximumLength(1000);
        RuleFor(x => x.Priority).Must(p => Enum.TryParse<TaskItemPriority>(p, out _))
            .WithMessage("Priority must be Low, Medium, or High.");
        RuleFor(x => x.ProjectId).GreaterThan(0);
    }
}

public class UpdateTaskItemDtoValidator : AbstractValidator<UpdateTaskItemDto>
{
    public UpdateTaskItemDtoValidator()
    {
        RuleFor(x => x.Title).MaximumLength(200).When(x => x.Title != null);
        RuleFor(x => x.Status).Must(s => Enum.TryParse<TaskItemStatus>(s, out _))
            .When(x => x.Status != null).WithMessage("Status must be Pending, InProgress, or Done.");
        RuleFor(x => x.Priority).Must(p => Enum.TryParse<TaskItemPriority>(p, out _))
            .When(x => x.Priority != null).WithMessage("Priority must be Low, Medium, or High.");
    }
}