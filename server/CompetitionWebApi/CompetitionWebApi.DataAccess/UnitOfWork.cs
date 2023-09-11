using CompetitionWebApi.DataAccess.Repositories;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly CompetitionDbContext _context;

    public IUsersRepository UserRepository { get; }
    public IPerformancesRepository PerformanceRepository { get; }
    public IScoresRepository ScoreRepository { get; }

    private bool isDisposed;

    public UnitOfWork(CompetitionDbContext context)
    {
        _context = context;
        UserRepository = new UsersRepository(_context);
        PerformanceRepository = new PerformancesRepository(_context);
        ScoreRepository = new ScoresRepository(_context);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!isDisposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            isDisposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
