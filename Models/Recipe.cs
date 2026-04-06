using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NutriCook_AI_WebAPI.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Difficulty { get; set; }


    }
}
