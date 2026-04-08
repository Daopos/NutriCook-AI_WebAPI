using NutriCook_AI_WebAPI.Data;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Repositories
{
    public class RecipeRepository : Repository<Recipe>, IRecipeRepo
    {
        private readonly AppDbContext _context;

        public RecipeRepository(AppDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Recipe> CreateNewRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }
    }
}
