using System.Net;

namespace CompetitionWebApi.Application.Interfaces;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    public string ErrorMessage { get; }
}
