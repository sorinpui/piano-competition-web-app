namespace CompetitionWebApi.Application.Interfaces;

public interface IValidationService
{
    Task ValidateRequest<T>(T request);
}
