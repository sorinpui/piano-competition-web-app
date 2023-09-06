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

    public async Task CreatePerformanceAsync(Performance entity)
    {
        await _context.AddAsync(entity);
    }

    public async Task<Performance?> GetPerformanceByIdAsync(int id)
    {
        return await _context.Performances.FindAsync(id);
    }
}
