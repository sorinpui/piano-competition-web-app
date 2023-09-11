using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class ScoreValidator : AbstractValidator<ScoreRequest>
{
    private readonly string _emptyField = "This field cannot be empty.";

    public ScoreValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleForEach(x => new[] { x.Interpretation, x.Technicality, x.Difficulty })
            .NotEmpty().WithMessage(_emptyField)
            .InclusiveBetween(10, 100).WithMessage("The score must be between 10 and 100.")
            .WithName("something");

        RuleFor(x => x.PerformanceId).NotEmpty().WithMessage(_emptyField);
    }
}
