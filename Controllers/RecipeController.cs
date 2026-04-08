using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutriCook_AI_WebAPI.DTOs.AIRecipe;
using NutriCook_AI_WebAPI.ExternalServices;

namespace NutriCook_AI_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {

        private readonly IAIRecipeGenerator _aiRecipeGenerator;

        public RecipeController(IAIRecipeGenerator aIRecipeGenerator)
        {
            _aiRecipeGenerator = aIRecipeGenerator;
        }

        [HttpGet("gemini")]
        public async Task<IActionResult> GenerateRecipe([FromBody] AIRecipeRequest req)
        {
          
                var recipe = await _aiRecipeGenerator.GenerateRecipe(req);
                return Ok(recipe);
        }

    }
}
