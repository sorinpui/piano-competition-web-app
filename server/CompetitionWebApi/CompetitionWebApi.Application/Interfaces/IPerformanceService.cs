using CompetitionWebApi.Application.Requests;

namespace CompetitionWebApi.Application.Interfaces;

public interface IPerformanceService
{
    Task SavePerformanceVideo(string boundary, Stream requestBody, int performanceId);
    Task CreatePerformanceInfoAsync(PerformanceRequest request);
}
