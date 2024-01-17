using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;
using CompetitionApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CompetitionApi.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class RenditionController : ControllerBase
    {
        private readonly IValidationService _validationService;
        private readonly IRenditionService _renditionService;

        public RenditionController(IValidationService validationService, IRenditionService renditionService)
        {
            _validationService = validationService;
            _renditionService = renditionService;
        }

        [HttpPost]
        [DisableFormValueModelBinding]
        [RequestSizeLimit(3145728001)]
        [Authorize(Roles = "Contestant")]
        public async Task<IActionResult> UploadRendition()
        {
            var request = HttpContext.Request;

            if (!_validationService.IsMultipartRequest(request))
            {
                return new UnsupportedMediaTypeResult();
            }

            var response = await _renditionService.CreateRenditionAsync(request);

            return Created(string.Empty, response);
        }

        [HttpGet("videos/{renditionId}")]
        [Authorize(Roles = "Spectator")]
        public async Task<IActionResult> DownloadRenditionVideo(int renditionId)
        {
            var (fileName, fileStream) = await _renditionService.DownloadRenditionVideo(renditionId);

            if (fileStream == null)
            {
                return NotFound(new ApiResponse<string?>(false, "There was an error downloading the video.", null));
            }

            return File(fileStream, "application/octet-stream", fileName);
        }

        [HttpGet]
        [Authorize(Roles = "Spectator")]
        public async Task<IActionResult> GetAllRenditions()
        {
            var response = await _renditionService.GetAllRenditionsAsync();

            return Ok(response);
        }

        [HttpGet("{renditionId}")]
        [Authorize(Roles = "Spectator")]
        public async Task<IActionResult> GetRendition(int renditionId)
        {
            var response = await _renditionService.GetRenditionByIdAsync(renditionId);

            if (response.IsSuccess == false)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
