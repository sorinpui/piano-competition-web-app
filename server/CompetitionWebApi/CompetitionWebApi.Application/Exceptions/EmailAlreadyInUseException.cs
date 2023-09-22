using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class EmailAlreadyInUseException : Exception, IServiceException
{
    public HttpStatusCode Status => HttpStatusCode.Conflict;

    public string ErrorMessage { get; init; }

    public EmailAlreadyInUseException() { }
}
