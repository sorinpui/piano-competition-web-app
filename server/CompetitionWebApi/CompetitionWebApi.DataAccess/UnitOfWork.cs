using CompetitionWebApi.DataAccess.Repositories;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly CompetitionDbContext _context;

    public IUserRepository UserRepository { get; }
    public IPerformanceRepository PerformanceRepository { get; }
    public IScoreRepository ScoreRepository { get; }
    public ICommentRepository CommentRepository { get; }

    private bool isDisposed;

    public UnitOfWork(CompetitionDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
        PerformanceRepository = new PerformanceRepository(_context);
        ScoreRepository = new ScoreRepository(_context);
        CommentRepository = new CommentRepository(_context);
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
