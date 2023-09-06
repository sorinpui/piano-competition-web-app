using CompetitionWebApi.Application.Requests;


namespace CompetitionWebApi.Application.Interfaces;

public interface IAccountService
{
    Task RegisterUserAsync(RegisterRequest request);
    Task<string> LoginUserAsync(LoginRequest request);
}
