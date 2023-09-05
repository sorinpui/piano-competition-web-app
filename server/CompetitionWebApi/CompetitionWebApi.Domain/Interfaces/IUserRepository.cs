using CompetitionWebApi.Domain.Entities;

namespace CompetitionWebApi.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(User entity);
    Task<User> GetUserByEmail(string email);
}
