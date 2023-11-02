using CompetitionWebApi.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CompetitionWebApi.Application.Interfaces;

public interface IAccountService
{
    Task<IActionResult> RegisterUserAsync(RegisterRequest request);
    Task<IActionResult> LoginUserAsync(LoginRequest request);
}
