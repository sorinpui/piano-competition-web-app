namespace CompetitionWebApi.Application.Interfaces;

public interface IPerformanceService
{
    Task CreatePerformance(string boundary, Stream requestBody);
}
