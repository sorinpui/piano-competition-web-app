using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Helpers;
using CompetitionWebApi.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace CompetitionWebApi.Application.Services;

public class ValidationService : IValidationService
{
    private readonly IValidatorsFactory _validatorsFactory;

    public ValidationService(IValidatorsFactory validatorsFactory)
    {
        _validatorsFactory = validatorsFactory;
    }

    public async Task ValidateRequestAsync<T>(T request)
    {
        await _validatorsFactory.GetValidator<T>().ValidateAndThrowAsync(request);
    }

    public string ValidateMultipartRequest(HttpRequest request)
    {
        string? contentType = request.ContentType;

        if (!MultipartRequestHelper.IsMultipartContentType(contentType))
        {
            throw new InvalidRequestException() 
            {
                ErrorMessage = "The media type must be multipart/form-data."
            };
        }

        int maxBoundaryLength = 70;

        string boundary = MultipartRequestHelper.GetBoundary(MediaTypeHeaderValue.Parse(contentType), maxBoundaryLength);

        return boundary;
    }
}
