using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitionApi.DataAccess.Repositories
{
    public class RenditionRepository : IRenditionRepository
    {
        private readonly CompetitionDbContext _context;

        public RenditionRepository(CompetitionDbContext context)
        {
            _context = context;
        }

        public async Task CreateRenditionAsync(Rendition entity)
        {
            await _context.AddAsync(entity);
        }

        public async Task<List<Rendition>> FindRenditionsByOwnerIdAsync(int ownerId)
        {
            var renditions = await _context.Renditions
                .Where(r => EF.Property<Rendition>(r, "PerformerId").Equals(ownerId))
                .ToListAsync();

            return renditions;
        }
        
        public async Task<Rendition?> FindRenditionByIdAsync(int id)
        {
            var rendition =  await _context.Renditions
                .Include(r => r.User)
                .Include(r => r.Score)
                .Where(r => r.Id == id)
                .FirstOrDefaultAsync();

            return rendition;
        }

        public async Task<List<Rendition>> GetAllRenditionsAsync()
        {
            return await _context.Renditions
                .Include(x => x.User)
                .Include(x => x.Score)
                .ToListAsync();
        }
    }
}
