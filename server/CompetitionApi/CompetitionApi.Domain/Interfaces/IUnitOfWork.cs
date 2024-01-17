namespace CompetitionApi.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IRoleRepository RoleRepository { get; }
        public IRenditionRepository RenditionRepository { get; }
        public IScoreRepository ScoreRepository { get; }
        public ICommentRepository CommentRepository { get; }

        Task SaveAllChangesAsync();
    }
}
