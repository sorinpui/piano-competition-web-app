using CompetitionApi.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace CompetitionApi.Application.Services
{
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

        public bool IsMultipartRequest(HttpRequest request)
        {
            return
                request.HasFormContentType &&
                MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) &&
                !string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value);
        }
    }
}
