using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Exceptions;

internal class EntityNotFoundException : Exception, IServiceException
{
    public HttpStatusCode StatusCode => HttpStatusCode.NotFound;

    public string ErrorMessage { get; }

    public EntityNotFoundException(string errorMessage)
    {
        ErrorMessage = errorMessage;
    }
}
