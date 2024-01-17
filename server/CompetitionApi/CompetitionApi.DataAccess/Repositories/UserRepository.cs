using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitionApi.DataAccess.Repositories
{
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

        public async Task<User?> FindUserByEmail(string email)
        {
            return await _context.Users
                .Where(u => u.Email.Equals(email))
                .Include(u => u.Roles)
                .FirstOrDefaultAsync();
        }
        
        public async Task<User?> FindUserById(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .Include(u => u.Roles)
                .FirstOrDefaultAsync();
        }
    }
}
