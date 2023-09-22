using CompetitionWebApi.Application.Dtos;
using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Responses;
using CompetitionWebApi.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CompetitionWebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PerformancesController : ControllerBase
{
    private readonly IPerformancesService _performanceService;
    private readonly IValidationService _validationService;

    public PerformancesController(IPerformancesService performanceService, IValidationService validationService)
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

        var response = new SuccessResponse<string>()
        {
            Message = "Performance information created successfully.",
            Status = HttpStatusCode.Created,
            Payload = string.Empty
        };

        return Created(string.Empty, response);
    }

    [HttpPost("videos")]
    [DisableFormValueModelBinding]
    [RequestSizeLimit(314_572_800)] // 300 MB
    [Authorize(Roles = "Contestant")]
    public async Task<IActionResult> UploadPerformanceVideo([FromQuery] int performanceId)
    {
        string boundary = _validationService.ValidateMultipartRequest(Request);

        await _performanceService.SavePerformanceVideoAsync(boundary, Request.Body, performanceId);

        return Created(string.Empty, new SuccessResponse<string> { Status = HttpStatusCode.Created, Payload = string.Empty });
    }

    [HttpGet("videos")]
    [Authorize]
    public async Task<IActionResult> DownloadPerformanceVideo([FromQuery] int performanceId)
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
            Payload = performances,
            Status = HttpStatusCode.OK
        };

        return Ok(response);
    }
}
