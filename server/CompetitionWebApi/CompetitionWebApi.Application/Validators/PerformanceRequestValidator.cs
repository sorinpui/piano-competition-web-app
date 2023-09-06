using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Enums;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class PerformanceRequestValidator : AbstractValidator<PerformanceRequest>
{
    private readonly string _emptyFieldMessage = "This form field cannot be empty.";

    public PerformanceRequestValidator()
    {
        RuleLevelCascadeMode = CascadeMode.Stop;

        RuleFor(req => req.PieceName).NotEmpty().WithMessage(_emptyFieldMessage);
        RuleFor(req => req.Composer).NotEmpty().WithMessage(_emptyFieldMessage);
        RuleFor(req => req.Period).NotEmpty().WithMessage(_emptyFieldMessage).IsInEnum();
        RuleFor(req => req.UserId).NotEmpty().WithMessage(_emptyFieldMessage);
    }
}
