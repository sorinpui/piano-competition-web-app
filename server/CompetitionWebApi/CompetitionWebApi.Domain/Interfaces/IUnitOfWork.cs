namespace CompetitionWebApi.Domain.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }

    Task SaveAsync();
}
