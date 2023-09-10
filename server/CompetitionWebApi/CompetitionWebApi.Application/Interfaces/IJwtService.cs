namespace CompetitionWebApi.Application.Interfaces;

public interface IJwtService
{  
    public string CreateToken(int roleId, int userId);
    public int GetNameIdentifier();
}
