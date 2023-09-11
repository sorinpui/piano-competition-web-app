using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class EmailAlreadyInUseException : Exception, IServiceException
{
    public HttpStatusCode Status => HttpStatusCode.Conflict;

    public string Title { get; set; }
    public string Detail { get; set; }

    public EmailAlreadyInUseException() { }
}
