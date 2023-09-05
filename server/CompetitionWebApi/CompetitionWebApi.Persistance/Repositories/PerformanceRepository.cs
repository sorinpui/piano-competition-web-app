using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.DataAccess.Repositories;

public class PerformanceRepository : IPerformanceRepository
{
    private readonly CompetitionDbContext _context;

    public PerformanceRepository(CompetitionDbContext context)
    {
        _context = context;
    }

    public async Task CreatePerformance(Performance entity)
    {
        await _context.AddAsync(entity);
    }
}
