using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitionApi.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CompetitionDbContext _context;

        public RoleRepository(CompetitionDbContext context)
        {
            _context = context;
        }

        public async Task<List<Role>> FindRolesByNameInAsync(List<string> roles)
        { 
            return await _context.Roles
                .Where(x => roles.Contains(x.Name))
                .ToListAsync();
        }
    }
}
