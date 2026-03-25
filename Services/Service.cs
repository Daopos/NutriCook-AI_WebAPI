using NutriCook_AI_WebAPI.Data;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Interfaces.IServices;

namespace NutriCook_AI_WebAPI.Services
{
    public class Service<T> : IService<T> where T : class
    {

        protected readonly IRepository<T> _repository;

        protected readonly AppDbContext _context;

        public Service(IRepository<T> repository, AppDbContext context)
        {
            this._repository = repository;
            this._context = context;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _repository.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                throw new Exception("Entity not found");

            _repository.Delete(entity);
            await _context.SaveChangesAsync();
        }

    }
}
