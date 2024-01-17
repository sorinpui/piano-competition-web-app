using CompetitionApi.Domain.Entities;

namespace CompetitionApi.Application.Interfaces
{
    public interface IJwtService
    {
        string CreateToken(List<Role> roles, int userId);
        int GetSubjectClaim();
    }
}
