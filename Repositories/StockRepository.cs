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


    }
}
