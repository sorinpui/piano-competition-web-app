using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class EmailAlreadyInUseException : Exception, IServiceException
{
    public string Email { get; }
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
    public string ErrorMessage => $"{Email} is already in use.";

    public EmailAlreadyInUseException(string email) : base(email)
    {
        Email = email;
    }
}
