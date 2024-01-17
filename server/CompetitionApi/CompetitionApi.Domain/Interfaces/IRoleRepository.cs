using CompetitionApi.Domain.Entities;

namespace CompetitionApi.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> FindRolesByNameInAsync(List<string> roles);
    }
}
