using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Interfaces.IRepo
{
    public interface IRecipeRepo
    {

        public Task<Recipe> CreateNewRecipe(Recipe recipe);

    }
}
