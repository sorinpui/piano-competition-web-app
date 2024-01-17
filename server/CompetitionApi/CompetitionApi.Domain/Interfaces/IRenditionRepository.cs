using CompetitionApi.Domain.Entities;

namespace CompetitionApi.Domain.Interfaces
{
    public interface IRenditionRepository
    {
        Task CreateRenditionAsync(Rendition entity);
        Task<List<Rendition>> FindRenditionsByOwnerIdAsync(int ownerId);
        Task<Rendition?> FindRenditionByIdAsync(int renditionId);
        Task<List<Rendition>> GetAllRenditionsAsync();
    }
}
