namespace CompetitionWebApi.Application.Interfaces;

public interface IJwtService
{
    public string CreateToken(IEnumerable<int> roles, int userId);
    public int GetSubjectClaim();
}
