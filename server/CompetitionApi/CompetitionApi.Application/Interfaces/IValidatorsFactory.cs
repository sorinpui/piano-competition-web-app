using FluentValidation;

namespace CompetitionApi.Application.Interfaces
{
    public interface IValidatorsFactory
    {
        IValidator<T> GetValidator<T>();
    }
}
