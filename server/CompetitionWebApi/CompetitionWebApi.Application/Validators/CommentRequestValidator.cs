using CompetitionWebApi.Application.Requests;
using FluentValidation;

namespace CompetitionWebApi.Application.Validators;

public class CommentRequestValidator : AbstractValidator<CommentRequest>
{
    private const string _emptyField = "This field is empty.";

    public CommentRequestValidator()
    {
        RuleFor(req => req.Message).NotEmpty().WithMessage(_emptyField);
        RuleFor(req => req.PerformanceId).NotEmpty().WithMessage(_emptyField);
        RuleFor(req => req.UserId).NotEmpty().WithMessage(_emptyField);
    }
}
