using CompetitionWebApi.Application.Interfaces;
using FluentValidation;

namespace CompetitionWebApi.Application.Services;

public class ValidationService : IValidationService
{
    private readonly IValidatorsFactory _validatorsFactory;

    public ValidationService(IValidatorsFactory validatorsFactory)
    {
        _validatorsFactory = validatorsFactory;
    }

    public async Task ValidateRequest<T>(T request)
    {
        await _validatorsFactory.GetValidator<T>().ValidateAndThrowAsync(request);
    }
}
