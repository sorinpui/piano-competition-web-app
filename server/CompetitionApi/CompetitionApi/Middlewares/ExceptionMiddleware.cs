using CompetitionApi.Application.Exceptions;
using CompetitionApi.Application.Responses;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace CompetitionApi.Middlewares
{
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

                var validationResponse = new ValidationErrorResponse("One or more fields are not valid.", errors);

                string result = JsonSerializer.Serialize(validationResponse);
                await context.Response.WriteAsync(result);
            }
            else if (ex is ServiceException serviceException)
            {
                context.Response.StatusCode = (int)serviceException.StatusCode;

                var response = new ApiResponse<string>(false, serviceException.Message, null);
                string result = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(result);
            }
        }
    }
}
