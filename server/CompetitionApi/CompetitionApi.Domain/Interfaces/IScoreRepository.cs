using CompetitionApi.Domain.Entities;

namespace CompetitionApi.Domain.Interfaces
{
    public interface IScoreRepository
    {
        Task CreateScoreAsync(Score entity);
        Task<Score?> FindScoreByRenditionIdAsync(int renditionId);
    }
}
