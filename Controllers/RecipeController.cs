using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpGet("stocks")]
        public async Task<IActionResult> GenerateRecipeBaseByStock()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            var recipes = await _aiRecipeGenerator.GenerateRecipeBaseByStock(userId);

            return Ok(recipes); ;
        }
    }
}
