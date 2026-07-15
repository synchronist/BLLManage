using FluentValidation;

namespace BLLManage.Application.Features.Companies.Create;

public sealed class CreateCompanyValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Company name is required.")
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Company email is required.")
            .EmailAddress().WithMessage("Company email is invalid.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Company phone is required.")
            .MaximumLength(30);
    }
}