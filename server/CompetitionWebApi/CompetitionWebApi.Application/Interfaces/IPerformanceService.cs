using CompetitionWebApi.Application.Requests;

namespace CompetitionWebApi.Application.Interfaces;

public interface IPerformanceService
{
    Task CreatePerformanceInfoAsync(PerformanceRequest request);
    Task SavePerformanceVideoAsync(string boundary, Stream requestBody, int performanceId);
    Task<(Stream, string)> GetPerformanceVideoAsync(int performanceId);
}
