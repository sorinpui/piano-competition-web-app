using CompetitionApi.Application.Requests;
using FluentValidation;

namespace CompetitionApi.Application.Validators
{
    public class PostCommentRequestValidator : AbstractValidator<PostCommentRequest>
    {
        public PostCommentRequestValidator() 
        {
            RuleFor(r => r.Message).NotEmpty().WithMessage("This field is required.");
            RuleFor(r => r.RenditionId).NotEmpty().WithMessage("This field is required.");
        }
    }
}
