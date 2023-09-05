using CompetitionWebApi.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;

namespace CompetitionWebApi.Application.Factories;

public class ValidatorsFactory : IValidatorsFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ValidatorsFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IValidator<T> GetValidator<T>()
    {
        return _serviceProvider.GetRequiredService<IValidator<T>>();
    }
}
