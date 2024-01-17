using CompetitionApi.Application.Requests;
using FluentValidation;

namespace CompetitionApi.Application.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("This field is required.");
        }
    }
}
