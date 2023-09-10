using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class ForbiddenException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

    public string ErrorMessage { get; }

    public ForbiddenException(string message)
    {
        ErrorMessage = message;
    }
}
