using System.ComponentModel.DataAnnotations;

namespace NutriCook_AI_WebAPI.DTOs.Recipe
{
    public class CreateRecipeDTO
    {
        [Required]
        public string Ingredients { get; set; }
    }
}
