using System.Collections;
using NutriCook_AI_WebAPI.DTOs.AIRecipe;
using NutriCook_AI_WebAPI.DTOs.Recipe;

namespace NutriCook_AI_WebAPI.ExternalServices
{
    public interface IAIRecipeGenerator
    {

        Task<AIRecipeResponse> GenerateRecipe(AIRecipeRequest req);

        Task<IEnumerable<AIRecipeResponse>> GenerateRecipeBaseByStock(int userId);

    }
}
