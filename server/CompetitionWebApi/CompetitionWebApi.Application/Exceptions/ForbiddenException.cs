using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class ForbiddenException : Exception, IServiceException
{
    public HttpStatusCode Status => HttpStatusCode.Forbidden;

    public string ErrorMessage { get; init; }

    public ForbiddenException() { }
}
