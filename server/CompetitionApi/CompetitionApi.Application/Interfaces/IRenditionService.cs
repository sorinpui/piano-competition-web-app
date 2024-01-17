using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Responses;
using Microsoft.AspNetCore.Http;

namespace CompetitionApi.Application.Interfaces
{
    public interface IRenditionService
    {
        Task<ApiResponse<string>> CreateRenditionAsync(HttpRequest request);
        Task<(string?, FileStream?)> DownloadRenditionVideo(int renditionId);
        Task<ApiResponse<List<RenditionSummaryDto>>> GetAllRenditionsAsync();
        Task<ApiResponse<RenditionDto>> GetRenditionByIdAsync(int renditionId);
    }
}
