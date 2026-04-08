using NutriCook_AI_WebAPI.DTOs.Users;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Interfaces.IServices;
using NutriCook_AI_WebAPI.Middleware.Exceptions;
using NutriCook_AI_WebAPI.Models;
using NutriCook_AI_WebAPI.Util;

namespace NutriCook_AI_WebAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthService(IUserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService; // You can inject this if it has dependencies
        }

        public async Task<UserTokenResponse?> LoginUser(UserLoginDTO user)
        {

            var existingUser = await _userRepository.GetUserByEmail(user.Email);

            if (existingUser is null)
            {
                throw new NotFoundException("user is not existing");
            }

            if(!BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                throw new BadRequestException("Invalid Credentials");
            }


            var token = _tokenService.CreateToken(existingUser);

            return new UserTokenResponse()
            {
                AccessToken = token
            };
        }

        public async Task<UserTokenResponse> SignupUser(UserCreateDTO user)
        {

            if (await _userRepository.GetUserByEmail(user.Email) is not null)
            {
                throw new NotFoundException("Email is already exists");
            }

            var mapUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password) // Hash the password before storing
            };


            var newUser = await _userRepository.CreateUser(mapUser);

            var token = _tokenService.CreateToken(newUser);

            if (string.IsNullOrEmpty(token))
            {
                throw new Exception("Token creation failed");
            }


            return new UserTokenResponse()
            {
                AccessToken = token
            };
        }

    }
}
