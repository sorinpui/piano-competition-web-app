using System.Net;

namespace CompetitionWebApi.Application.Interfaces;

public interface IServiceException
{
    public string ErrorMessage { get; }
    public HttpStatusCode Status { get; }
}
