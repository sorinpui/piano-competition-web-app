using CompetitionWebApi.Application.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CompetitionWebApi.Application.Validators;

public class ReigsterRequestValidator : AbstractValidator<RegisterRequest>
{
    private const string _passwordError = "The password must be at least 8 characters long containing lowercase and uppercase letters, at least one symbol and at least one digit.";
    private const string _empty = "This field cannot be empty.";

    public ReigsterRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.FirstName)
            .NotEmpty().WithMessage(_empty);
        RuleFor(request => request.LastName)
            .NotEmpty().WithMessage(_empty);

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(_empty)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(_empty)
            .Must(BeStrong).WithMessage(_passwordError);

        RuleFor(request => request.RoleId)
            .NotEmpty().WithMessage(_empty)
            .IsInEnum().WithMessage("The value provided is not valid.")
            .NotEqual(Domain.Enums.Role.Judge).WithMessage("The value provided is not valid.");
    }

    private bool BeStrong(string password)
    {
        if (password == null) return false;

        Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$");

        return regex.IsMatch(password);
    }
}
