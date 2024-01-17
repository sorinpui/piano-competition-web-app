using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CompetitionApi.Controllers
{
    [Route("api/[controller]s")]
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
        public async Task<ApiResponse<string>> RegisterUser([FromBody] CreateUserRequest request)
        {
            await _validationService.ValidateRequestAsync(request);

            var result = await _accountService.RegisterUserAsync(request);

            HttpContext.Response.StatusCode = (int)result.StatusCode;

            return new ApiResponse<string>(result.OperationSucceeded, result.Message, null);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest request)
        {
            await _validationService.ValidateRequestAsync(request);

            var response = await _accountService.LoginUserAsync(request);

            return Ok(response);
        }

        [HttpPost("admin")]
        [Authorize(Roles = "Admin")]
        public async Task<ApiResponse<string>> CreateUserAccount([FromBody] CreateUserRequest request)
        {
            await _validationService.ValidateRequestAsync(request);

            var result = await _accountService.CreateUserAsync(request);

            HttpContext.Response.StatusCode = (int)result.StatusCode;

            return new ApiResponse<string>(result.OperationSucceeded, result.Message, null);
        }
    }
}
