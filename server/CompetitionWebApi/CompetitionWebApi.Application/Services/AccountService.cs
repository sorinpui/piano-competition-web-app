using CompetitionWebApi.Domain.Entities;
using CompetitionWebApi.Domain.Interfaces;
using CompetitionWebApi.Application.Requests;
using CompetitionWebApi.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CompetitionWebApi.Application.Responses;

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

    public async Task<IActionResult> RegisterUserAsync(RegisterRequest request)
    {
        User? user = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);

        if (user != null)
        {
            return new ConflictObjectResult(new ErrorResponse
            {
                Title = "Email Conflict",
                Detail = "There's already an account associated with this email."
            });
        }

        string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password);

        User newUser = Mapper.RegisterUserRequestToUserEntity(request, passwordHash);

        await _unitOfWork.UserRepository.CreateUserAsync(newUser);
        await _unitOfWork.SaveAsync();

        return new CreatedResult(string.Empty, new SuccessResponse<string>
        {
            Message = "Account created successfully",
            Payload = null
        });
    }

    public async Task<IActionResult> LoginUserAsync(LoginRequest request)
    {
        User? userFromDb = await _unitOfWork.UserRepository.GetUserByEmailAsync(request.Email);
        bool isMatch = false;

        if (userFromDb != null)
        {
            isMatch = BCrypt.Net.BCrypt.EnhancedVerify(request.Password, userFromDb.Password);
        }

        if (userFromDb == null || !isMatch)
        {
            return new UnauthorizedObjectResult(new ErrorResponse
            {
                Title = "Bad Credentials",
                Detail = "The email or password is incorrect."
            });
        }

        string token = _jwtService.CreateToken(userFromDb.RoleId, userFromDb.Id);

        return new OkObjectResult(new SuccessResponse<string>
        {
            Message = "Logged in successfully.",
            Payload = token
        });
    }
}
