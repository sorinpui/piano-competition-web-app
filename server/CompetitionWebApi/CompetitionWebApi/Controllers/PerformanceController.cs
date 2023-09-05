using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Helpers;
using CompetitionWebApi.Application.Interfaces;
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

    [HttpPost]
    [DisableFormValueModelBinding]
    [RequestSizeLimit(300 * 1024 * 10 ^ 6)]
    [Authorize]
    public async Task<IActionResult> UploadPerformance()
    {
        string boundary = _validationService.ValidateMultipartRequest(Request);

        await _performanceService.CreatePerformance(boundary, Request.Body);

        return Created(string.Empty, new SuccessResponse<string> { StatusCode = HttpStatusCode.Created, Payload = string.Empty });
    }
}
