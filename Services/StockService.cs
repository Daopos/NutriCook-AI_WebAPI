using NutriCook_AI_WebAPI.Data;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Interfaces.IServices;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Services
{
    public class StockService : Service<Stock>, IStockService
    {
        public StockService(IRepository<Stock> repository, AppDbContext context) : base(repository, context)
        {
        }
    }
}
