using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Responses;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace CompetitionWebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        HttpResponse httpResponse = context.Response;

        if (ex is ValidationException validationException)
        {
            httpResponse.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = validationException
                .Errors
                .ToDictionary(e => e.PropertyName[..1].ToLower() + e.PropertyName[1..], e => e.ErrorMessage);

            Console.WriteLine(errors.Count);

            var validationResponse = new ValidationErrorResponse()
            {
                Title = "Invalid Request",
                Detail = "One or more fields are not valid.",
                Errors = errors
            };

            string result = JsonSerializer.Serialize(validationResponse);
            await context.Response.WriteAsync(result);
        }
        else
        {
            var (title, detail, statusCode) = ex switch
            {
                ServiceException serviceEx => (serviceEx.Title, serviceEx.ErrorMessage, serviceEx.StatusCode),
                _ => ("Internal Server Error", null, HttpStatusCode.InternalServerError)
            };

            var response = new ErrorResponse()
            {
                Title = title,
                Detail = detail
            };

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
}
