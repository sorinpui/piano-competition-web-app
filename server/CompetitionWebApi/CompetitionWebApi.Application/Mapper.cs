using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Application.Requests;

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

    internal static Performance PerformanceRequestToPerformanceEntity(PerformanceRequest request)
    {
        return new Performance
        {
            Piece = new Piece
            {
                Name = request.PieceName,
                Composer = request.Composer,
                Period = request.Period
            },

            UserId = request.UserId
        };
    }
}
