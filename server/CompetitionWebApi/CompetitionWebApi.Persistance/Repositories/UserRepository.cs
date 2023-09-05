using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitionWebApi.Persistance.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CompetitionDbContext _context;

    public UserRepository(CompetitionDbContext context)
    {
        _context = context;
    }

    public async Task CreateUserAsync(User entity)
    {
        await _context.AddAsync(entity);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        User? userFromDb = await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

        return userFromDb;
    }
}
