using Microsoft.EntityFrameworkCore;
using NutriCook_AI_WebAPI.Data;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Repositories
{
    public class StockRepository : Repository<Stock>, IStockRepo
    {

        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }



        public async Task<IEnumerable<(string Name, int Quantity)>> GetAllStocksWithQuantityAsync(int userId)
        {
            var stocks = await _context.Stocks
                                .Where(s => s.Quantity > 0 && s.UserId == userId)
                                .Select(s => new ValueTuple<string, int>(s.Name, s.Quantity))
                                .ToListAsync();

            return stocks;
        }


    }
}
