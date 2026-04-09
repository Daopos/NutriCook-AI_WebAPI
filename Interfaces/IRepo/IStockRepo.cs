namespace NutriCook_AI_WebAPI.Interfaces.IRepo
{
    public interface IStockRepo
    {


        Task<IEnumerable<(string Name, int Quantity)>> GetAllStocksWithQuantityAsync(int userId);

    }
}
