﻿using CompetitionWebApi.Domain.Entities;

namespace CompetitionWebApi.Domain.Interfaces;

public interface IPerformanceRepository
{
    Task CreatePerformanceAsync(Performance entity);
    Task<Performance?> GetPerformanceByIdAsync(int id);
    Task<List<Performance>> GetAllPerformancesAsync();
    Task<List<Performance>> GetPerformancesByUserId(int userId);
}
