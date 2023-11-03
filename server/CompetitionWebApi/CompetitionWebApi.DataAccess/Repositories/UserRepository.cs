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

    public async Task CreateUserAsync(User entity, List<int> roles)
    {
        await _context.Users.AddAsync(entity);
        await _context.SaveChangesAsync();

        foreach (int role in roles)
        {
            await _context.UserRoles.AddAsync(new UserRole
            {
                UserId = entity.Id,
                RoleId = role
            });
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        User? user = await _context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

        return user;
    }

    public async Task<User?> GetUserByIdAsync(int userId)
    {
        User? user = await _context.Users.FindAsync(userId);
        
        return user;
    }
     
    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<List<int>> GetUserRolesById(int userId)
    {
        List<int> roles = await _context.UserRoles
            .Where(x => x.UserId == userId)
            .Select(y => y.RoleId)
            .ToListAsync();

        return roles;
    }
}
