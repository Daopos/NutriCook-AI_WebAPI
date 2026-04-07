using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Interfaces.IRepo
{
    public interface IUserRepository
    {

        Task<User> CreateUser(User data);

        Task<User?> GetUserById(int id);
    }
}
