namespace CompetitionWebApi.Domain.Interfaces;

public interface IUnitOfWork
{
    IUsersRepository UserRepository { get; }
    IPerformancesRepository PerformanceRepository { get; }
    IScoresRepository ScoreRepository { get; }

    Task SaveAsync();
}
