using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class PerformanceRequestValidator : AbstractValidator<PerformanceRequest>
{
    private readonly string _emptyFieldError = "This field is empty.";

    public PerformanceRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(req => req.PieceName).NotEmpty().WithMessage(_emptyFieldError);
        RuleFor(req => req.Composer).NotEmpty().WithMessage(_emptyFieldError);
        RuleFor(req => req.Period)
            .NotEmpty().WithMessage(_emptyFieldError)
            .IsInEnum().WithMessage("Invalid period. 1 - Baroque, 2 - Classical, 3 - Romantic");
        RuleFor(req => req.UserId).NotEmpty().WithMessage(_emptyFieldError);
    }
}
