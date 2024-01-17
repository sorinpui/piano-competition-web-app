using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;
using System.Net;

namespace CompetitionApi.Application.Interfaces
{
    public interface IAccountService
    {
        Task<UserCreationResult> RegisterUserAsync(CreateUserRequest request);
        Task<UserCreationResult> CreateUserAsync(CreateUserRequest request);
        Task<ApiResponse<string>> LoginUserAsync(LoginRequest request);
    }
}
