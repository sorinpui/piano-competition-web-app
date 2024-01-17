using CompetitionApi.DataAccess.Repositories;
using CompetitionApi.Domain.Interfaces;

namespace CompetitionApi.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CompetitionDbContext _context;
        
        public IUserRepository UserRepository { get; private set; }
        public IRoleRepository RoleRepository { get; private set; }
        public IRenditionRepository RenditionRepository { get; private set; }
        public IScoreRepository ScoreRepository { get; private set; }
        public ICommentRepository CommentRepository { get; private set; }

        public UnitOfWork(CompetitionDbContext context)
        {
            _context = context;
            UserRepository = new UserRepository(context);
            RoleRepository = new RoleRepository(context);
            RenditionRepository = new RenditionRepository(context);
            ScoreRepository = new ScoreRepository(context);
            CommentRepository = new CommentRepository(context);
        }

        public async Task SaveAllChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
