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
            RoleId = request.RoleId
        };
    }
}
