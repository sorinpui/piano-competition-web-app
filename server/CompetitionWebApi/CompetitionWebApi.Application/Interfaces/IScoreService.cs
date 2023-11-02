using CompetitionWebApi.Application.Requests;

namespace CompetitionWebApi.Application.Interfaces;

public interface IScoreService
{
    Task CreateScoreAsync(ScoreRequest request);
}
