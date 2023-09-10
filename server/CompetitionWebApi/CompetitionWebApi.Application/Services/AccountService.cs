using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;

using CompetitionWebApi.Application.Exceptions;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Interfaces;

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
            throw new EmailAlreadyInUseException(request.Email);
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
            throw new EntityNotFoundException($"There's no account registered with {request.Email}");
        }

        bool isMatch = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, userFromDb.Password);

        if (!isMatch)
        {
            throw new IncorrectPasswordException();
        }

        string token = _jwtService.CreateToken(userFromDb.RoleId, userFromDb.Id);

        return token;
    }
}
