using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        Performance? performance = await _context.Performances.FindAsync(id);

        return performance;
    }

    public async Task<List<Performance>> GetAllPerformancesAsync()
    {
        List<Performance> performances = await _context.Performances.ToListAsync();

        return performances;
    }

    public async Task<List<Performance>> GetPerformancesByUserId(int userId)
    {
        List<Performance> performances = await _context.Performances
            .Where(x => x.UserId.Equals(userId))
            .ToListAsync();

        return performances;
    }
}
