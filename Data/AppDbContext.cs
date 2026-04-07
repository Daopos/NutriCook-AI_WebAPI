using Microsoft.EntityFrameworkCore;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Stock> Stocks {get; set;}
        public DbSet<User> Users { get; set; }

    }
}
