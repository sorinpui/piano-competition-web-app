using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Net;

namespace CompetitionWebApi.Controllers;

[ApiController]
public class ErrorController : ControllerBase
{
    public Dictionary<HttpStatusCode, string> DocumentationOf { get; }
    
    public ErrorController()
    {
        DocumentationOf = new()
        {
            { HttpStatusCode.InternalServerError, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1" },
            { HttpStatusCode.BadRequest, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1" },
            { HttpStatusCode.Conflict, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8" },
            { HttpStatusCode.Unauthorized, "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1" },
            { HttpStatusCode.NotFound, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4" }
        };
    }

    [Route("/error")]
    protected ErrorResponse HandleError()
    {
        HttpContext.Response.Headers["Content-Type"] = "application/problem+json";

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        Console.WriteLine(exception?.Message);

        if (exception is ValidationException validationException)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return new ValidationErrorResponse
            {
                Type = DocumentationOf[HttpStatusCode.BadRequest],
                Title = "One or more fields are not valid.",
                Status = (int)HttpStatusCode.BadRequest,
                Errors = validationException.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage)
            };
        }

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => (serviceException.StatusCode, serviceException.ErrorMessage),
            _ => (HttpStatusCode.InternalServerError, "Internal server error.")
        };

        HttpContext.Response.StatusCode = (int)statusCode;

        return new ErrorResponse
        {
            Type = DocumentationOf[statusCode],
            Title = message,
            Status = (int)statusCode
        };
    }
}
