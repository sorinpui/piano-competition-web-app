namespace CompetitionWebApi.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IPerformanceRepository PerformanceRepository { get; }
    IScoreRepository ScoreRepository { get; }
    ICommentRepository CommentRepository { get; }

    Task SaveAsync();
}
