namespace NutriCook_AI_WebAPI.Interfaces.IServices
{
    public interface IService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task CreateAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(int id);

    }
}
