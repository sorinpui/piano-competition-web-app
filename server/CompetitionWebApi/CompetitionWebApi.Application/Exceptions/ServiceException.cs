using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public abstract class ServiceException : Exception
{
    public string Title { get; set; }
    public string ErrorMessage { get; set; }
    public HttpStatusCode Status { get; init; }
}
