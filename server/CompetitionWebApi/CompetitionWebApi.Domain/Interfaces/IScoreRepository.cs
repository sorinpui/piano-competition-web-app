using CompetitionWebApi.Domain.Entities;

namespace CompetitionWebApi.Domain.Interfaces;

public interface IScoreRepository
{
    Task CreateScoreAsync(Score entity);
}
