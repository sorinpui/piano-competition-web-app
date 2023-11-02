using CompetitionWebApi.Application.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using System.Net;
using CompetitionWebApi.Application.Exceptions;

namespace CompetitionWebApi.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    public ErrorResponse HandleError()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationException)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = validationException.Errors
                .ToDictionary(e => e.PropertyName[..1].ToLower() + e.PropertyName[1..], e => e.ErrorMessage);

            return new ValidationErrorResponse
            {
                Title = "Bad Request",
                Detail = "One or more fields are not valid.",
                Errors = errors
            };
        }

        var (title, errorMessage, status) = exception switch
        {
            ServiceException ex => (ex.Title, ex.ErrorMessage, ex.Status),
            _ => ("Internal Server Error", null, HttpStatusCode.InternalServerError)
        };

        HttpContext.Response.StatusCode = (int)status;

        return new ErrorResponse
        {
            Title = title,
            Detail = errorMessage,
        };
    }
}
