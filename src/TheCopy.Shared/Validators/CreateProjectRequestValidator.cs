using FluentValidation;
using TheCopy.Shared.Models;

namespace TheCopy.Shared.Validators;

public class CreateProjectRequestValidator : AbstractValidator<CreateProjectRequest>
{
    public CreateProjectRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Project name is required")
            .MinimumLength(3)
            .WithMessage("Project name must be at least 3 characters")
            .MaximumLength(100)
            .WithMessage("Project name must not exceed 100 characters");

        RuleFor(x => x.Description)
            .MaximumLength(2000)
            .WithMessage("Description must not exceed 2000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Deadline)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Deadline must be in the future")
            .When(x => x.Deadline.HasValue);
    }
}
