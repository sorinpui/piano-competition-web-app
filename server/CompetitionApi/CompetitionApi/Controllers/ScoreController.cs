using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompetitionApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        private readonly IScoreService _scoreService;
        private readonly IValidationService _validationService;

        public ScoreController(IScoreService scoreService, IValidationService validationService)
        {
            _scoreService = scoreService;
            _validationService = validationService;
        }

        [HttpPost]
        [Authorize(Roles = "Baroque_Judge, Classical_Judge, Romantic_Judge")]
        public async Task<IActionResult> AssignScore([FromBody] CreateScoreRequest request)
        {
            await _validationService.ValidateRequestAsync(request);

            var result = await _scoreService.CreateScoreAsync(request);
            int statusCode = (int)result.StatusCode;

            var response = new ApiResponse<string>(result.OperationSucceeded, result.Message, null);

            switch (statusCode)
            {
                case StatusCodes.Status404NotFound:
                    return NotFound(response);

                case StatusCodes.Status403Forbidden:
                    return StatusCode(statusCode, response);

                case StatusCodes.Status409Conflict:
                    HttpContext.Response.Headers.Location = result.UriLocation;
                    HttpContext.Response.StatusCode = statusCode;

                    return Conflict(response);
                
                case StatusCodes.Status201Created:
                    return Created(string.Empty, response);
                
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
