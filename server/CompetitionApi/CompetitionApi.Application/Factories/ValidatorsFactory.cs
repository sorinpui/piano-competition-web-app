using CompetitionApi.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CompetitionApi.Application.Factories
{
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
}
