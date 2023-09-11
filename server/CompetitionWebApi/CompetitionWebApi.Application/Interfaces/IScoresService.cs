using CompetitionWebApi.Application.Requests;

namespace CompetitionWebApi.Application.Interfaces;

public interface IScoresService
{
    Task CreateScoreAsync(ScoreRequest request);
}
