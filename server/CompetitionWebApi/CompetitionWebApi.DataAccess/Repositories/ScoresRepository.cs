using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.DataAccess.Repositories;

public class ScoresRepository : IScoresRepository
{
    private readonly CompetitionDbContext _context;

    public ScoresRepository(CompetitionDbContext context)
    {
        _context = context;
    }

    public async Task CreateScoreAsync(Score entity)
    {
        await _context.AddAsync(entity);
    }
}
