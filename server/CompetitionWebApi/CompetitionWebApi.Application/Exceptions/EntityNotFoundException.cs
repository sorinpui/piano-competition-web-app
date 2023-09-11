using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class EntityNotFoundException : Exception, IServiceException
{
    public HttpStatusCode Status => HttpStatusCode.NotFound;

    public string Title { get; set; }
    public string Detail { get; set; }

    public EntityNotFoundException() { }
}
