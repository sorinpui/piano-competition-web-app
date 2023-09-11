using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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

        await _accountService.RegisterUserAsync(request);

        SuccessResponse<string> response = new()
        {
            Message = "Account registered successfully.",
            Payload = "",
            Status = HttpStatusCode.Created 
        };

        return Created(string.Empty, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
    {
        await _validationService.ValidateRequestAsync(request);

        string token = await _accountService.LoginUserAsync(request);

        SuccessResponse<string> response = new()
        {
            Message = "Logged in successfully.",
            Payload = token,
            Status = HttpStatusCode.OK
        };

        return Ok(response);
    }
}
