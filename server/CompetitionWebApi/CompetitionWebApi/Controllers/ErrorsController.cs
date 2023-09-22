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
    [Route("/error")]
    public ErrorResponse HandleError()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (exception is ValidationException validationException)
        {
            HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return new ValidationErrorResponse
            {
                ErrorMessage = "One or more fields are not valid.",
                Status = HttpStatusCode.BadRequest,
                Errors = validationException.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage)
            };
        }

        var (errorMessage, status) = exception switch
        {
            IServiceException ex => (ex.ErrorMessage, ex.Status),
            _ => ("Internal server error.", HttpStatusCode.InternalServerError)
        };

        HttpContext.Response.StatusCode = (int)status;

        return new ErrorResponse
        {
            ErrorMessage = errorMessage,
            Status = status
        };
    }
}
