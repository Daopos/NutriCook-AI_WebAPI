using System.ComponentModel.DataAnnotations;

namespace NutriCook_AI_WebAPI.DTOs.Recipe
{
    public class ReadRecipeDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }

    }
}
