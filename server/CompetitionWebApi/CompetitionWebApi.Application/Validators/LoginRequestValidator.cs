using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    private const string _emptyFieldMessage = "This field cannot be empty.";

    public LoginRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(_emptyFieldMessage)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(_emptyFieldMessage);
    }
}
