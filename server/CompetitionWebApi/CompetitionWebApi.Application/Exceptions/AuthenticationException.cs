using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class AuthenticationException : ServiceException
{
    public AuthenticationException(HttpStatusCode statusCode)
    {
        StatusCode = statusCode;
    }
}
