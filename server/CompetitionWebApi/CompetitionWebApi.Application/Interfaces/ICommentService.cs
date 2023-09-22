using CompetitionWebApi.Application.Requests;

namespace CompetitionWebApi.Application.Interfaces;

public interface ICommentService
{
    Task PostCommentAsync(CommentRequest request);
}
