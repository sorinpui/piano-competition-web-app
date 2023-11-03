using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CompetitionWebApi.Application.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    private const string _passwordError = "The password must be at least 8 characters long containing lowercase and uppercase letters, at least one symbol and at least one digit.";
    private const string _empty = "This field cannot be empty.";

    public RegisterRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage(_empty);
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(_empty);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage(_empty)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(_empty)
            .Must(BeStrong).WithMessage(_passwordError);

        RuleFor(x => x.Roles)
            .NotEmpty().WithMessage(_empty)
            .Must(HaveValidRoles).WithMessage("The roles provided are not valid.");
    }

    private static bool BeStrong(string password)
    {
        if (password == null) return false;

        Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$");

        return regex.IsMatch(password);
    }

    private static bool HaveValidRoles(List<RoleType> roles)
    {
        foreach (RoleType role in roles)
        {
            if (!Enum.IsDefined(typeof(RoleType), role) || role == RoleType.Judge)
            {
                return false;
            }
        }

        return true;
    }
}
