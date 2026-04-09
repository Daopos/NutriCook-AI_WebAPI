using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Interfaces.IServices
{
    public interface IStockService : IService<Stock>
    {

        Task<IEnumerable<Stock>> GetStocksByUserId(int userId);

    }
}
