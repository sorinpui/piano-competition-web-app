using CompetitionWebApi.Domain.Entities;

namespace CompetitionWebApi.Domain.Interfaces;

public interface ICommentRepository
{
    Task CreateCommentAsync(Comment entity);
}
