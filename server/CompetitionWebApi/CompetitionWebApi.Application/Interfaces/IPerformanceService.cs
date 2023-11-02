using CompetitionWebApi.Application.Dtos;
using CompetitionWebApi.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CompetitionWebApi.Application.Interfaces;

public interface IPerformanceService
{
    Task<IActionResult> CreatePerformanceInfoAsync(PerformanceRequest request);
    Task SavePerformanceVideoAsync(string boundary, Stream requestBody, int performanceId);
    Task<PerformanceVideoDto> GetPerformanceVideoAsync(int performanceId);
    Task<List<PerformanceDto>> GetAllPerformancesAsync();
}
