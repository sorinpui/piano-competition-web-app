using CompetitionWebApi.Domain.Entities;

namespace CompetitionWebApi.Domain.Interfaces;

public interface IPerformanceRepository
{
    Task CreatePerformance(Performance entity);
}
