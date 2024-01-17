using CompetitionApi.Domain.Entities;

namespace CompetitionApi.Domain.Interfaces
{
    public interface ICommentRepository
    {
        Task CreateCommentAsync(Comment entity);
        Task<List<Comment>> FindAllCommentsByRenditionId(int renditionId);
    }
}
