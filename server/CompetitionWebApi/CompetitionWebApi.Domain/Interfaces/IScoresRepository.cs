using CompetitionWebApi.Domain.Entities;

namespace CompetitionWebApi.Domain.Interfaces;

public interface IScoresRepository
{
    Task CreateScoreAsync(Score entity);
}
