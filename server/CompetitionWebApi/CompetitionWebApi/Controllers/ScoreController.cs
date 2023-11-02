using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompetitionWebApi.Controllers
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
        [Authorize(Roles = "Judge")]
        public async Task<IActionResult> AssignScores([FromBody] ScoreRequest request)
        {
            await _validationService.ValidateRequestAsync(request);

            await _scoreService.CreateScoreAsync(request);

            return Created(string.Empty, new SuccessResponse<string> { Payload = string.Empty });
        }
    }
}
