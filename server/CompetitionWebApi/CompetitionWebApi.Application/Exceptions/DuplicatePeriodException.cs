using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

public class DuplicatePeriodException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;

    public string ErrorMessage { get; }

    public DuplicatePeriodException(string message) : base(message)
    {
        ErrorMessage = message;
    }
}
