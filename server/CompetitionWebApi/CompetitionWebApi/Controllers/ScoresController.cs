using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompetitionWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoresController : ControllerBase
    {
        private readonly IScoresService _scoreService;
        private readonly IValidationService _validationService;

        public ScoresController(IScoresService scoreService, IValidationService validationService)
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

            return Created(string.Empty, new SuccessResponse<string> { Status = HttpStatusCode.Created, Payload = string.Empty });
        }
    }
}
