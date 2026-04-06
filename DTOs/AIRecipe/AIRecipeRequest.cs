using System.ComponentModel.DataAnnotations;

namespace NutriCook_AI_WebAPI.DTOs.AIRecipe
{
    public class AIRecipeRequest
    {
        [Required]
        public string Ingredients { get; set; }
    }
}
