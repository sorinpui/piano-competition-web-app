using System.Net;

namespace CompetitionWebApi.Application.Interfaces;

public interface IServiceException
{
    public HttpStatusCode Status { get; }
    public string Title { get; set; }
    public string Detail { get; set; }
}
