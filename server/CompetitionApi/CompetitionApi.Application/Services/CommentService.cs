using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;
using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Interfaces;

namespace CompetitionApi.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public CommentService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<OperationResult<string>> CreateCommentAsync(PostCommentRequest request)
        {
            Rendition? rendition = await _unitOfWork.RenditionRepository.FindRenditionByIdAsync(request.RenditionId);

            if (rendition == null)
            {
                return new OperationResult<string>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    OperationSucceeded = false,
                    Message = "The rendition with the specified id doesn't exist."
                };
            }

            int userId = _jwtService.GetSubjectClaim();

            User? user = await _unitOfWork.UserRepository.FindUserById(userId);

            if (user == null)
            {
                return new OperationResult<string>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    OperationSucceeded = false,
                    Message = "The user you're logged in as doesn't exist."
                };
            }

            Comment newComment = Mapper.PostCommentRequestToCommentEntity(request, rendition, user);

            await _unitOfWork.CommentRepository.CreateCommentAsync(newComment);
            await _unitOfWork.SaveAllChangesAsync();

            return new OperationResult<string>
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                OperationSucceeded = true,
                Message = "Comment posted successfully."
            };
        }

        public async Task<OperationResult<List<CommentDto>>> GetCommentsByRenditionIdAsync(int renditionId)
        {
            Rendition? rendition = await _unitOfWork.RenditionRepository.FindRenditionByIdAsync(renditionId);

            if (rendition == null)
            {
                return new OperationResult<List<CommentDto>>
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound,
                    OperationSucceeded = false,
                    Message = "The rendition with the specified id doesn't exist."
                };
            }

            List<Comment> commentsFromDb = await _unitOfWork.CommentRepository.FindAllCommentsByRenditionId(renditionId);
            List<CommentDto> comments = commentsFromDb.Select(Mapper.CommentEntityToCommentDto).ToList();

            return new OperationResult<List<CommentDto>>
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                OperationSucceeded = true,
                Message = "Comments retrieved successfully.",
                Payload = comments
            };
        }
    }
}
