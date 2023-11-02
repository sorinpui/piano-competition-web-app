using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.Application.Services;

public class ScoreService : IScoreService
{
    private readonly IUnitOfWork _unitOfWork;

    public ScoreService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateScoreAsync(ScoreRequest request)
    {
        Performance? performanceFromDb = await _unitOfWork.PerformanceRepository.GetPerformanceByIdAsync(request.PerformanceId);

        if (performanceFromDb == null)
        {
            throw new EntityNotFoundException()
            {
                Title = "Performance Not Found",
                ErrorMessage = "The performance you want to score doesn't exist."
            };
        }

        Score newScore = Mapper.ScoreRequestToScoreEntity(request);

        await _unitOfWork.ScoreRepository.CreateScoreAsync(newScore);
        await _unitOfWork.SaveAsync();
    }
}
