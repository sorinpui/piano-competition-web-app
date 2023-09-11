using CompetitionWebApi.Application.Requests;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CompetitionWebApi.Application.Validators;

public class ReigsterRequestValidator : AbstractValidator<RegisterRequest>
{
    private const string _passwordError = "Requirements: 8 characters, at least one uppercase letter, one symbol and one digit.";
    private const string _emptyFieldError = "This field is empty.";

    public ReigsterRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.FirstName)
            .NotEmpty().WithMessage(_emptyFieldError);
        RuleFor(request => request.LastName)
            .NotEmpty().WithMessage(_emptyFieldError);

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(_emptyFieldError)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(_emptyFieldError)
            .Must(BeStrong).WithMessage(_passwordError);

        RuleFor(request => request.RoleId)
            .NotEmpty().WithMessage(_emptyFieldError)
            .IsInEnum().WithMessage("There is no role associated with your input.")
            .NotEqual(Domain.Enums.Role.Judge).WithMessage("This role is for judges only.");
    }

    private bool BeStrong(string password)
    {
        if (password == null) return false;

        Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$");

        return regex.IsMatch(password);
    }
}
