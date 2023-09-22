﻿using CompetitionWebApi.Application.Dtos;
using CompetitionWebApi.Application.Requests;

namespace CompetitionWebApi.Application.Interfaces;

public interface IPerformancesService
{
    Task CreatePerformanceInfoAsync(PerformanceRequest request);
    Task SavePerformanceVideoAsync(string boundary, Stream requestBody, int performanceId);
    Task<PerformanceVideoDto> GetPerformanceVideoAsync(int performanceId);
    Task<List<PerformanceDto>> GetAllPerformancesAsync();
}
