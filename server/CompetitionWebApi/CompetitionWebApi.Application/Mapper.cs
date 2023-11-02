using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Dtos;

namespace CompetitionWebApi.Application;

public static class Mapper
{
    public static User RegisterUserRequestToUserEntity(RegisterRequest request, string passwordHash)
    {
        return new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Password = passwordHash,
            RoleId = (int)request.RoleId
        };
    }

    public static Performance PerformanceRequestToPerformanceEntity(PerformanceRequest request, int userId)
    {
        return new Performance
        {
            Piece = new Piece
            {
                Name = request.PieceName,
                Composer = request.Composer,
                Period = request.Period
            },
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };
    }

    public static Score ScoreRequestToScoreEntity(ScoreRequest request)
    {
        return new Score
        {
            Interpretation = request.Interpretation,
            Technicality = request.Technicality,
            Difficulty = request.Difficulty,
            PerformanceId = request.PerformanceId
        };
    }

    public static PerformanceDto PerformanceEntityToPerformanceDto(Performance performance, User user)
    {
        return new PerformanceDto
        {
            PerformanceId = performance.Id,
            ContestantName = $"{user.FirstName} {user.LastName}",
            Piece = performance.Piece.Name,
            Composer = performance.Piece.Composer,
            Period = performance.Piece.Period.ToString(),
        };
    }

    public static Comment CommentRequestToCommentEntity(CommentRequest request)
    {
        return new Comment
        {
            Message = request.Message,
            PerformanceId = request.PerformanceId,
            UserId = request.UserId
        };
    }
}
