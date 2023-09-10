using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class JwtClaimsException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Forbidden;

    public string ErrorMessage { get; }

    public JwtClaimsException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
