using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class InvalidRequestException : Exception, IServiceException
{
    public HttpStatusCode Status => HttpStatusCode.BadRequest;

    public string ErrorMessage { get; init; }

    public InvalidRequestException() { }
}
