using CompetitionWebApi.Domain.Entities;

namespace CompetitionWebApi.Domain.Interfaces;

public interface IUserRepository
{
    Task CreateUserAsync(User entity, List<int> roles);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(int userId);
    Task<List<User>> GetAllUsersAsync();
    Task<List<int>> GetUserRolesById(int userId);
}
