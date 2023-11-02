using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    private const string _emptyField = "This field cannot be empty.";

    public LoginRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(_emptyField);

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(_emptyField);
    }
}
