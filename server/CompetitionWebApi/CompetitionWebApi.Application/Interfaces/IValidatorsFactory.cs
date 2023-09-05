using FluentValidation;

namespace CompetitionWebApi.Application.Interfaces;

public interface IValidatorsFactory
{
    IValidator<T> GetValidator<T>();
}
