namespace CompetitionWebApi.Application.Interfaces;

public interface IJwtService
{
    public string CreateToken(int roleId);
}
