using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitionApi.DataAccess.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly CompetitionDbContext _context;

        public ScoreRepository(CompetitionDbContext context)
        {
            _context = context;
        }

        public async Task CreateScoreAsync(Score entity)
        {
            await _context.Scores.AddAsync(entity);
        }

        public async Task<Score?> FindScoreByRenditionIdAsync(int renditionId)
        {
            return await _context.Scores
                .Where(s => s.RenditionId == renditionId)
                .FirstOrDefaultAsync();
        }
    }
}
