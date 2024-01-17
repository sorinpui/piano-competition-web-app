using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Interfaces;
using System.Net;

namespace CompetitionApi.Application.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public ScoreService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<ScoreCreationResult> CreateScoreAsync(CreateScoreRequest request)
        {
            Rendition? rendition = await _unitOfWork.RenditionRepository.FindRenditionByIdAsync(request.RenditionId);

            if (rendition == null)
            {
                return new ScoreCreationResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    OperationSucceeded = false,
                    Message = "The rendition with the specified id doesn't exist."
                };
            }

            Score? score = await _unitOfWork.ScoreRepository.FindScoreByRenditionIdAsync(rendition.Id);

            if (score != null)
            {
                return new ScoreCreationResult
                {
                    StatusCode = HttpStatusCode.Conflict,
                    OperationSucceeded = true,
                    Message = "This rendition is already scored.",
                    UriLocation = $"/api/scores/{score.Id}"
                };
            }

            int userId = _jwtService.GetSubjectClaim();
            User? judge = await _unitOfWork.UserRepository.FindUserById(userId);

            if (judge == null)
            {
                return new ScoreCreationResult
                {
                    StatusCode = HttpStatusCode.NotFound,
                    OperationSucceeded = false,
                    Message = "The user you're currently logged in as doesn't exist."
                };
            }

            foreach (Role role in judge.Roles)
            {
                if (role.Name.Contains(rendition.Piece.Period.ToString()))
                {
                    Score newScore = Mapper.CreateScoreRequestToScoreEntity(request, judge);

                    await _unitOfWork.ScoreRepository.CreateScoreAsync(newScore);
                    await _unitOfWork.SaveAllChangesAsync();

                    return new ScoreCreationResult
                    {
                        StatusCode = HttpStatusCode.Created,
                        OperationSucceeded = true,
                        Message = "Score created successfully."
                    };
                }
            }

            return new ScoreCreationResult
            { 
                StatusCode = HttpStatusCode.Forbidden,
                OperationSucceeded = false,
                Message = "You cannot score this rendition." 
            };
        }
    }
}
