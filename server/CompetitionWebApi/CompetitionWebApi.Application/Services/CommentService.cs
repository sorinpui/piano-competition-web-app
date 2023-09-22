using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

namespace CompetitionWebApi.Application.Services;

public class CommentService : ICommentService
{
    private readonly IUnitOfWork _unitOfWork;

    public CommentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task PostCommentAsync(CommentRequest request)
    {
        Performance? performance = await _unitOfWork.PerformanceRepository.GetPerformanceByIdAsync(request.PerformanceId);

        if (performance == null)
        {
            throw new EntityNotFoundException
            {
                ErrorMessage = $"The performance with id {request.PerformanceId} doesn't exist."
            };
        }
        
        User? user = await _unitOfWork.UserRepository.GetUserByIdAsync(request.UserId);

        Comment comment = Mapper.CommentRequestToCommentEntity(request);

        await _unitOfWork.CommentRepository.CreateCommentAsync(comment);
        await _unitOfWork.SaveAsync();
    }
}
