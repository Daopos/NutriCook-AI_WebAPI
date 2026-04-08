using Microsoft.EntityFrameworkCore;
using NutriCook_AI_WebAPI.Data;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<User> CreateUser(User data)
        {
            _context.Users.Add(data);
            await _context.SaveChangesAsync();

            return data;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            return user;
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            return user;
        }

    }
}
