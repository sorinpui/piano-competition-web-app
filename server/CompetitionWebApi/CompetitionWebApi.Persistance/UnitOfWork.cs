using CompetitionWebApi.Domain.Interfaces;
using CompetitionWebApi.Persistance;
using CompetitionWebApi.Persistance.Repositories;


namespace CompetitionWebApi.DataAccess;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly CompetitionDbContext _context;
    public IUserRepository UserRepository { get; }

    private bool isDisposed;

    public UnitOfWork(CompetitionDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(_context);
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
