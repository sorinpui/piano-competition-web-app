using Microsoft.AspNetCore.Http;

namespace CompetitionWebApi.Application.Interfaces;

public interface IValidationService
{
    Task ValidateRequestAsync<T>(T request);
    string ValidateMultipartRequest(HttpRequest request);
}
