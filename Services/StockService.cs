using System.Runtime.CompilerServices;
using NutriCook_AI_WebAPI.Data;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Interfaces.IServices;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Services
{
    public class StockService : Service<Stock>, IStockService
    {

        private readonly IStockRepo _stockRepo;


        public StockService(IRepository<Stock> repository, AppDbContext context, IStockRepo stockRepo) : base(repository, context)
        {
            _stockRepo = stockRepo;
        }

        public async Task<IEnumerable<Stock>> GetStocksByUserId(int userId)
        {

            var stocks = await _stockRepo.GetAllStocksByUserId(userId);

            return stocks;
        }

    }
}
