using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompetitionWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IValidationService _validationService;

    public AccountController(IAccountService accountService, IValidationService validationService)
    {
        _accountService = accountService;
        _validationService = validationService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterRequest request)
    {
        await _validationService.ValidateRequestAsync(request);

        IActionResult result = await _accountService.RegisterUserAsync(request);

        return result;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
    {
        await _validationService.ValidateRequestAsync(request);
        
        IActionResult result = await _accountService.LoginUserAsync(request);

        return result;
    }

    [HttpPost("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateJudge([FromBody] CreateJudgeRequest request)
    {
        await _validationService.ValidateRequestAsync(request);

        request.Roles = new List<RoleType>() { RoleType.Spectator, RoleType.Judge };

        IActionResult result = await _accountService.RegisterUserAsync(request);

        return result;
    }
}
