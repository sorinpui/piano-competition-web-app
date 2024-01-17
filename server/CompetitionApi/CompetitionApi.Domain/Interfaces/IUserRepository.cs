using CompetitionApi.Domain.Entities;

namespace CompetitionApi.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User entity);
        Task<User?> FindUserByEmail(string email);
        Task<User?> FindUserById(int id);
    }
}
