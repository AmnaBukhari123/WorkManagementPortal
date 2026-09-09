// src/Application/Validators/CommentValidators.cs
using FluentValidation;
using EnterpriseWorkManagementPortal.Application.DTOs;

namespace EnterpriseWorkManagementPortal.Application.Validators;

public class CreateCommentDtoValidator : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidator()
    {
        RuleFor(x => x.Content).NotEmpty().MaximumLength(2000);
        RuleFor(x => x.TaskItemId).GreaterThan(0);
        RuleFor(x => x.AuthorUserId).GreaterThan(0);
    }
}