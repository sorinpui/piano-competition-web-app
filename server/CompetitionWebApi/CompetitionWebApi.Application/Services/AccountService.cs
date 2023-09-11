using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Interfaces;
using System.Net;

namespace CompetitionWebApi.Application.Services;

public class AccountService : IAccountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtService _jwtService;

    public AccountService(IUnitOfWork unitOfWork, IJwtService jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task RegisterUserAsync(RegisterRequest request)
    {
        User? user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);

        if (user != null)
        {
            throw new EmailAlreadyInUseException()
            {
                Title = "Email Conflict",
                Detail = $"There's already an account associated with the email {request.Email}."
            };
        }

        string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);

        User newUser = Mapper.RegisterUserRequestToUserEntity(request, passwordHash);

        await _unitOfWork.UserRepository.CreateUserAsync(newUser);
        await _unitOfWork.SaveAsync();
    }

    public async Task<string> LoginUserAsync(LoginRequest request)
    {
        User? userFromDb = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);

        if (userFromDb == null)
        {
            throw new EntityNotFoundException()
            {
                Title = "Account Not Found",
                Detail = $"There's no account registered with the email {request.Email}"
            };
        }

        bool isMatch = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, userFromDb.Password);

        if (!isMatch)
        {
            throw new AuthenticationException(HttpStatusCode.Unauthorized)
            {
                Title = "Invalid Credentials",
                Detail = "The password is incorrect."
            };
        }

        string token = _jwtService.CreateToken(userFromDb.RoleId, userFromDb.Id);

        return token;
    }
}
