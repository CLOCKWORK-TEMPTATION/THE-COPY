using FluentValidation;
using TheCopy.Shared.Models;

namespace TheCopy.Shared.Validators;

public class UpdateScriptRequestValidator : AbstractValidator<UpdateScriptRequest>
{
    public UpdateScriptRequestValidator()
    {
        RuleFor(x => x.Title)
            .MinimumLength(3)
            .WithMessage("Title must be at least 3 characters")
            .MaximumLength(200)
            .WithMessage("Title must not exceed 200 characters")
            .When(x => !string.IsNullOrEmpty(x.Title));

        RuleFor(x => x.Content)
            .MinimumLength(1)
            .WithMessage("Content cannot be empty")
            .When(x => !string.IsNullOrEmpty(x.Content));

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .WithMessage("Description must not exceed 1000 characters")
            .When(x => !string.IsNullOrEmpty(x.Description));

        RuleFor(x => x.Status)
            .IsInEnum()
            .WithMessage("Invalid script status")
            .When(x => x.Status.HasValue);

        RuleFor(x => x.Tags)
            .Must(tags => tags == null || tags.Count <= 20)
            .WithMessage("Cannot have more than 20 tags")
            .Must(tags => tags == null || tags.All(tag => tag.Length <= 50))
            .WithMessage("Each tag must not exceed 50 characters")
            .When(x => x.Tags != null);
    }
}
