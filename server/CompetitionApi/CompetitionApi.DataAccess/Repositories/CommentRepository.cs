
using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CompetitionApi.DataAccess.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CompetitionDbContext _context;

        public CommentRepository(CompetitionDbContext context)
        {
            _context = context;
        }
        public async Task CreateCommentAsync(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
        }

        public async Task<List<Comment>> FindAllCommentsByRenditionId(int renditionId)
        {
            return await _context.Comments
                .Where(c => EF.Property<Comment>(c, "RenditionId").Equals(renditionId))
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}
