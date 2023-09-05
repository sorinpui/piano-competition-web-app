using CompetitionWebApi.Application.Requests;


namespace CompetitionWebApi.Application.Interfaces;

public interface IAccountService
{
    Task RegisterUser(RegisterRequest request);
    Task<string> LoginUser(LoginRequest request);
}
