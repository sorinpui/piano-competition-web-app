using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    private const string emptyFieldMessage = "This field cannot be empty.";

    public LoginRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(emptyFieldMessage)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(emptyFieldMessage);
    }
}
