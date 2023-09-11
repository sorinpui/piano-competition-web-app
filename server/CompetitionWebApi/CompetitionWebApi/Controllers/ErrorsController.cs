using CompetitionWebApi.Application.Interfaces;
using CompetitionWebApi.Application.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Net;

namespace CompetitionWebApi.Controllers;

[ApiController]
public class ErrorsController : ControllerBase
{
    public Dictionary<HttpStatusCode, string> DocumentationOf { get; }
    
    public ErrorsController()
    {
        DocumentationOf = new()
        {
            { HttpStatusCode.InternalServerError, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1" },
            { HttpStatusCode.BadRequest, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1" },
            { HttpStatusCode.Conflict, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8" },
            { HttpStatusCode.Unauthorized, "https://datatracker.ietf.org/doc/html/rfc7235#section-3.1" },
            { HttpStatusCode.NotFound, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4" },
            { HttpStatusCode.Forbidden, "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.3" }
        };
    }

    [Route("/error")]
    public ErrorResponse HandleError()
    {
        HttpContext.Response.Headers["Content-Type"] = "application/problem+json";

        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationException)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return new ValidationErrorResponse
            {
                Type = DocumentationOf[HttpStatusCode.BadRequest],
                Title = "Validation Error",
                Status = (int)HttpStatusCode.BadRequest,
                Detail = "One or more fields are not valid.",
                Errors = validationException.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage)
            };
        }

        var (statusCode, title, detail) = exception switch
        {
            IServiceException ex => (ex.Status, ex.Title, ex.Detail),
            _ => (HttpStatusCode.InternalServerError, "Internal server error.", string.Empty)
        };

        HttpContext.Response.StatusCode = (int)statusCode;

        return new ErrorResponse
        {
            Type = DocumentationOf[statusCode],
            Title = title,
            Status = (int)statusCode,
            Detail = detail
        };
    }
}
