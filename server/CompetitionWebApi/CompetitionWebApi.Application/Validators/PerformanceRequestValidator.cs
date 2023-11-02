using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class PerformanceRequestValidator : AbstractValidator<PerformanceRequest>
{
    private readonly string _empty = "This field cannot be empty.";

    public PerformanceRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(req => req.PieceName).NotEmpty().WithMessage(_empty);
        RuleFor(req => req.Composer).NotEmpty().WithMessage(_empty);
        RuleFor(req => req.Period)
            .NotEmpty().WithMessage(_empty)
            .IsInEnum().WithMessage("The value provided is not valid.");
    }
}
