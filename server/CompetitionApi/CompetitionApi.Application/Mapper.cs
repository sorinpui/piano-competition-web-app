using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Requests;
using CompetitionApi.Domain.ComplexTypes;
using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Enums;

namespace CompetitionApi.Application
{
    public static class Mapper
    {
        public static User CreateUserRequestToUserEntity(CreateUserRequest request, String passwordHash, List<Role> roles)
        {
            return new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                Roles = roles
            };
        }

        public static Rendition CreateRenditionRequestToRenditionEntity(CreateRenditionRequest renditionDto, string filePath, User performer)
        {
            Piece piece = new Piece()
            {
                Name = renditionDto.Name,
                Composer = renditionDto.Composer,
                Period = Enum.Parse<Period>(renditionDto.Period, true)
            };

            return new Rendition
            {
                Piece = piece,
                VideoUrl = filePath,
                User = performer
            };
        }

        public static RenditionSummaryDto RenditionEntityToRenditionSummaryDto(Rendition rendition)
        {
            return new RenditionSummaryDto
            {
                RenditionId = rendition.Id,
                Performer = $"{rendition.User.FirstName} {rendition.User.LastName}",
                Piece = rendition.Piece.Name,
                Composer = rendition.Piece.Composer,
                Period = rendition.Piece.Period.ToString(),
            };
        }

        public static RenditionDto RenditionEntityToRenditionDto(Rendition rendition, double? overallScore)
        {
            return new RenditionDto
            {
                RenditionId = rendition.Id,
                Performer = $"{rendition.User.FirstName} {rendition.User.LastName}",
                Piece = rendition.Piece.Name,
                Composer = rendition.Piece.Composer,
                Period = rendition.Piece.Period.ToString(),
                Interpretation = rendition.Score?.Interpretation,
                Difficulty = rendition.Score?.Difficulty,
                Technique = rendition.Score?.Technique,
                OverallScore = overallScore
            };
        }

        public static Score CreateScoreRequestToScoreEntity(CreateScoreRequest request, User judge)
        {
            return new Score
            {
                Interpretation = request.Interpretation,
                Difficulty = request.Difficulty,
                Technique = request.Technique,
                RenditionId = request.RenditionId,
                User = judge
            };
        }

        public static Comment PostCommentRequestToCommentEntity(PostCommentRequest request, Rendition rendition, User user)
        {
            return new Comment
            {
                Message = request.Message,
                Rendition = rendition,
                User = user
            };
        }

        public static CommentDto CommentEntityToCommentDto(Comment comment)
        {
            return new CommentDto
            {
                Message = comment.Message,
                PostedBy = $"{comment.User.FirstName} {comment.User.LastName}"
            };
        }
    }
}
