using CompetitionApi.Application.Requests;
using CompetitionApi.Domain.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CompetitionApi.Application.Validators
{
    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        private readonly string[] messages = 
        [
            "This field is required.",
            "The maximum length for this field is 50 characters.",
            "The password must be at least 8 characters long, contain at least one uppercase letter, one digit and one symbol.",
            "One or more role names are not valid."
        ];

        public CreateUserRequestValidator()
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage(messages[0])
                .MaximumLength(50).WithMessage(messages[1]);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(messages[0])
                .MaximumLength(50).WithMessage(messages[1]);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(messages[0])
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(messages[0])
                .Must(BeStrongEnough).WithMessage(messages[2]);

            RuleFor(x => x.Roles)
                .NotEmpty().WithMessage(messages[0])
                .Must(BeInEnum).WithMessage(messages[3]);
        }

        private static bool BeStrongEnough(string password)
        {
            Regex regex = new(@"^(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()\-_=+{};:,<.>]).{8,}$");

            return regex.IsMatch(password);
        }

        private static bool BeInEnum(List<string> roles)
        {
            return roles.All(r => Enum.TryParse<UserRole>(r, true, out _));
        }
    }
}
