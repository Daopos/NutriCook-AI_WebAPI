using NutriCook_AI_WebAPI.DTOs.AIRecipe;

namespace NutriCook_AI_WebAPI.ExternalServices
{
    public interface IAIRecipeGenerator
    {

        Task<AIRecipeResponse> GenerateRecipe(AIRecipeRequest req);

    }
}
