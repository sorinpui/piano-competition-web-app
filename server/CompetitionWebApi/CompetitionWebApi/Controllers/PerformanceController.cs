using CompetitionWebApi.Application.Dtos;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using CompetitionWebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompetitionWebApi.Controllers;

[Route("api/[controller]s")]
[ApiController]
public class PerformanceController : ControllerBase
{
    private readonly IPerformanceService _performanceService;
    private readonly IValidationService _validationService;

    public PerformanceController(IPerformanceService performanceService, IValidationService validationService)
    {
        _performanceService = performanceService;
        _validationService = validationService;
    }

    [HttpPost("information")]
    [Authorize(Roles = "Contestant")]
    public async Task<IActionResult> CreatePerformanceInfo([FromBody] PerformanceRequest request)
    {
        await _validationService.ValidateRequestAsync(request);

        await _performanceService.CreatePerformanceInfoAsync(request);

        return Created(string.Empty, new SuccessResponse<string> { StatusCode = HttpStatusCode.Created, Payload = string.Empty });
    }

    [HttpPost("videos")]
    [DisableFormValueModelBinding]
    [RequestSizeLimit(300*1024*10^6)] // 300 MB
    [Authorize(Roles = "Contestant")]
    public async Task<IActionResult> UploadPerformanceVideo([FromQuery] int performanceId)
    {
        string boundary = _validationService.ValidateMultipartRequest(Request);

        await _performanceService.SavePerformanceVideoAsync(boundary, Request.Body, performanceId);

        return Created(string.Empty, new SuccessResponse<string> { StatusCode = HttpStatusCode.Created, Payload = string.Empty });
    }

    [HttpGet("videos")]
    [Authorize]
    public async Task<IActionResult> DownloadPerformanceVideo([FromQuery] int performanceId)
    {
        PerformanceVideoDto result = await _performanceService.GetPerformanceVideoAsync(performanceId);

        return File(result.VideoStream, "video/mp4", result.FileName);
    }
}
