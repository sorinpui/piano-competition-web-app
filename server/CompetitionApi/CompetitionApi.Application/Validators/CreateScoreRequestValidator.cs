using CompetitionApi.Application.Requests;
using FluentValidation;

namespace CompetitionApi.Application.Validators
{
    public class CreateScoreRequestValidator : AbstractValidator<CreateScoreRequest>
    { 
        public CreateScoreRequestValidator() 
        {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.Interpretation)
                .NotEmpty().WithMessage("This field is required.")
                .InclusiveBetween(1, 10).WithMessage("The value must be between 1 and 10.");

            RuleFor(x => x.Difficulty)
                .NotEmpty().WithMessage("This field is required.")
                .InclusiveBetween(1, 10).WithMessage("The value must be between 1 and 10.");

            RuleFor(x => x.Technique)
                .NotEmpty().WithMessage("This field is required.")
                .InclusiveBetween(1, 10).WithMessage("The value must be between 1 and 10.");

            RuleFor(x => x.RenditionId)
                .NotEmpty().WithMessage("This field is required.");
        }
    }
}
