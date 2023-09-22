using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class AuthenticationException : Exception, IServiceException
{
    public HttpStatusCode Status { get; }

    public string ErrorMessage { get; init; }

    public AuthenticationException(HttpStatusCode statusCode)
    {
        Status = statusCode;
    }
}
