using CompetitionApi.Application.Dtos;
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
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IValidationService _validationService;

        public CommentController(ICommentService commentService, IValidationService validationService)
        {
            _commentService = commentService;
            _validationService = validationService;
        }

        [HttpPost]
        [Authorize(Roles = "Spectator")]
        public async Task<IActionResult> PostComment([FromBody] PostCommentRequest request)
        {
            await _validationService.ValidateRequestAsync(request);

            var result = await _commentService.CreateCommentAsync(request);
            var response = new ApiResponse<string>(result.OperationSucceeded, result.Message, null);

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }

            return Created(string.Empty, response);
        }

        [HttpGet("{renditionId}")]
        [Authorize(Roles = "Spectator")]
        public async Task<IActionResult> GetRenditionComments(int renditionId)
        {
            var result = await _commentService.GetCommentsByRenditionIdAsync(renditionId);
            var response = new ApiResponse<List<CommentDto>>(result.OperationSucceeded, result.Message, result.Payload);

            if (result.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
