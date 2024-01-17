using CompetitionApi.Application.Dtos;
using CompetitionApi.Application.Interfaces;
using CompetitionApi.Application.Requests;
using CompetitionApi.Application.Responses;
using CompetitionApi.Domain.Entities;
using CompetitionApi.Domain.Enums;
using CompetitionApi.Domain.Interfaces;
using System.Net;

namespace CompetitionApi.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;

        public AccountService(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<UserCreationResult> RegisterUserAsync(CreateUserRequest request)
        {
            foreach (string role in request.Roles)
            {
                if (role.Contains("judge", StringComparison.CurrentCultureIgnoreCase) || 
                    role.Contains("admin", StringComparison.CurrentCultureIgnoreCase))
                {
                    return new UserCreationResult
                    {
                        StatusCode = HttpStatusCode.Forbidden,
                        OperationSucceeded = false,
                        Message = "You cannot create an account with these roles."
                    };
                }
            }

            return await CreateUserAsync(request);
        }

        public async Task<UserCreationResult> CreateUserAsync(CreateUserRequest request)
        {
            string message = string.Empty;

            if (!request.Roles.Any(r => r.Contains("spectator", StringComparison.CurrentCultureIgnoreCase)))
            {
                request.Roles.Add(UserRole.Spectator.ToString());
            }

            User? user = await _unitOfWork.UserRepository.FindUserByEmail(request.Email);

            if (user != null)
            {
                return new UserCreationResult 
                {
                    StatusCode = HttpStatusCode.OK,
                    OperationSucceeded = false,
                    Message = "There's already an account registered with this email."
                };
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, 12);

            List<Role> roles = await _unitOfWork.RoleRepository.FindRolesByNameInAsync(request.Roles);

            User newUser = Mapper.CreateUserRequestToUserEntity(request, passwordHash, roles);

            await _unitOfWork.UserRepository.CreateUserAsync(newUser);
            await _unitOfWork.SaveAllChangesAsync();

            UserDto userInfo = new(newUser.FirstName + " " + newUser.LastName, newUser.Email);

            return new UserCreationResult
            { 
                StatusCode = HttpStatusCode.Created,
                OperationSucceeded = true,
                Message = "User account created successfully."
        };
        }

        public async Task<ApiResponse<string>> LoginUserAsync(LoginRequest request)
        {
            User? user = await _unitOfWork.UserRepository.FindUserByEmail(request.Email);
            
            if (user == null)
            {
                return new ApiResponse<string>(false, "Incorrect email or password.", null);
            }

            bool isMatch = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
            
            if (!isMatch)
            {
                return new ApiResponse<string>(false, "Incorrect email or password.", null);
            }
            
            string token = _jwtService.CreateToken(user.Roles, user.Id);

            return new ApiResponse<string>(true, "Logged in successfully.", token);
        }
    }
}
