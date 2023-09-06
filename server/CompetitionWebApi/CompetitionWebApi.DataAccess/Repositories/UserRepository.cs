using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitionWebApi.DataAccess.Repositories;

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

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? userFromDb = await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

        return userFromDb;
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        User? userFromDb = await _context.Users.FindAsync(id);

        return userFromDb;
    }
}
