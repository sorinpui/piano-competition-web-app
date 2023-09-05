namespace CompetitionWebApi.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IPerformanceRepository PerformanceRepository { get; }

    Task SaveAsync();
}
