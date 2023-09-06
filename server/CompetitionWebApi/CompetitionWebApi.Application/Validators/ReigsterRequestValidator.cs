using CompetitionWebApi.Application.Requests;
using FluentValidation;
using System.Text.RegularExpressions;


namespace CompetitionWebApi.Application.Validators;

public class ReigsterRequestValidator : AbstractValidator<RegisterRequest>
{
    private const string passwordErrorMessage = "Requirements: 8 characters, at least one uppercase letter, one symbol and one digit.";
    private const string emptyFieldMessage = "This field cannot be empty.";

    public ReigsterRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(request => request.FirstName)
            .NotEmpty().WithMessage(emptyFieldMessage);
        RuleFor(request => request.LastName)
            .NotEmpty().WithMessage(emptyFieldMessage);

        RuleFor(request => request.Email)
            .NotEmpty().WithMessage(emptyFieldMessage)
            .EmailAddress().WithMessage("Invalid email address.");

        RuleFor(request => request.Password)
            .NotEmpty().WithMessage(emptyFieldMessage)
            .Must(BeStrong).WithMessage(passwordErrorMessage);

        RuleFor(request => request.RoleId)
            .NotEmpty().WithMessage(emptyFieldMessage)
            .IsInEnum().WithMessage("There is no role associated with your input.");
    }

    private bool BeStrong(string password)
    {
        if (password == null) return false;

        Regex regex = new Regex(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$");

        return regex.IsMatch(password);
    }
}
