using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Requests;

namespace CompetitionApi.Application.Interfaces
{
    public interface IScoreService
    {
        Task<ScoreCreationResult> CreateScoreAsync(CreateScoreRequest request);
    }
}
