using Microsoft.AspNetCore.Http;
namespace CompetitionApi.Application.Interfaces
{
    public interface IValidationService
    {
        Task ValidateRequestAsync<T>(T request);
        bool IsMultipartRequest(HttpRequest request);
    }
}
