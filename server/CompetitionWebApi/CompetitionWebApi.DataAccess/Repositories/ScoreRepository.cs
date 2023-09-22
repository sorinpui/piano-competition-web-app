using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.DataAccess.Repositories;

public class ScoreRepository : IScoreRepository
{
    private readonly CompetitionDbContext _context;

    public ScoreRepository(CompetitionDbContext context)
    {
        _context = context;
    }

    public async Task CreateScoreAsync(Score entity)
    {
        await _context.AddAsync(entity);
    }
}
