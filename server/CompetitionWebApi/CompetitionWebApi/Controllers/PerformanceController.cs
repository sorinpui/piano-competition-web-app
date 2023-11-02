using CompetitionWebApi.Application.Dtos;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using CompetitionWebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        IActionResult result = await _performanceService.CreatePerformanceInfoAsync(request);

        return result;
    }

    [HttpPost("videos/{performanceId}")]
    [DisableFormValueModelBinding]
    [RequestSizeLimit(314_572_800)] // 300 MB
    [Authorize(Roles = "Contestant")]
    public async Task<IActionResult> UploadPerformanceVideo([FromRoute] int performanceId)
    {
        string boundary = _validationService.ValidateMultipartRequest(Request);

        await _performanceService.SavePerformanceVideoAsync(boundary, Request.Body, performanceId);

        var response = new SuccessResponse<string>
        {
            Message = "Performance video uploaded successfully.",
            Payload = null
        };

        return Created(string.Empty, response);
    }

    [HttpGet("videos/{performanceId}")]
    [Authorize]
    public async Task<IActionResult> DownloadPerformanceVideo([FromRoute] int performanceId)
    {
        PerformanceVideoDto result = await _performanceService.GetPerformanceVideoAsync(performanceId);

        return File(result.VideoStream, "video/mp4", result.FileName);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllPerformances()
    {
        var performances = await _performanceService.GetAllPerformancesAsync();

        var response = new SuccessResponse<List<PerformanceDto>>
        {
            Message = "Performances retrieved successfully.",
            Payload = performances
        };

        return Ok(response);
    }
}
