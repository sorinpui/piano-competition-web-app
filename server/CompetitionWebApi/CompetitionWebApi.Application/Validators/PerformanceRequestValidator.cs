using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class PerformanceRequestValidator : AbstractValidator<PerformanceRequest>
{
    private readonly string _emptyFieldMessage = "This form field cannot be empty.";

    public PerformanceRequestValidator()
    {
        RuleFor(req => req.PieceName).NotEmpty().WithMessage(_emptyFieldMessage);
        RuleFor(req => req.Composer).NotEmpty().WithMessage(_emptyFieldMessage);
        RuleFor(req => req.Period).NotEmpty().WithMessage(_emptyFieldMessage);
        RuleFor(req => req.UserId).NotEmpty().WithMessage(_emptyFieldMessage);
    }
}
