using NutriCook_AI_WebAPI.DTOs.Users;

namespace NutriCook_AI_WebAPI.Interfaces.IServices
{
    public interface IAuthService
    {

        Task<UserTokenResponse?> LoginUser(UserLoginDTO user);

        Task<UserTokenResponse> SignupUser(UserCreateDTO user);

    }
}
