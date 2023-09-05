using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

internal class UserNotFoundException : Exception, IServiceException
{
    public string Email { get; }

    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage => $"There's no account registered with {Email}";

    public UserNotFoundException(string email)
    {
        Email = email;
    }
}
