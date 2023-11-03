using CompetitionWebApi.Application.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CompetitionWebApi.Application.Validators;

public class CreateJudgeRequestValidator : AbstractValidator<CreateJudgeRequest>
{
    private const string _passwordError = "The password must be at least 8 characters long containing lowercase and uppercase letters, at least one symbol and at least one digit.";
    private readonly string _empty = "This field cannot be empty.";

    public CreateJudgeRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FirstName).NotEmpty().WithMessage(_empty);
        RuleFor(x => x.LastName).NotEmpty().WithMessage(_empty);
        RuleFor(x => x.Email).NotEmpty().WithMessage(_empty);
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(_empty)
            .Must(BeStrong).WithMessage(_passwordError);
    }

    private bool BeStrong(string password)
    {
        if (password == null) return false;

        Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$");

        return regex.IsMatch(password);
    }
}
