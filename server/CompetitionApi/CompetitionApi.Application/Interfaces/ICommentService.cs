using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;

namespace CompetitionApi.Application.Interfaces
{
    public interface ICommentService
    {
        Task<OperationResult<string>> CreateCommentAsync(PostCommentRequest request);
        Task<OperationResult<List<CommentDto>>> GetCommentsByRenditionIdAsync(int renditionId);
    }
}
