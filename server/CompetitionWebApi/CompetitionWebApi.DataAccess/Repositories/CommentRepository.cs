using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.DataAccess.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly CompetitionDbContext _context;

    public CommentRepository(CompetitionDbContext context)
    {
        _context = context;
    }

    public async Task CreateCommentAsync(Comment entity)
    {
        await _context.AddAsync(entity);
    }
}
