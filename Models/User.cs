using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriCook_AI_WebAPI.Models
{
    public class User : BaseModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Role { get; set; } = "User";

        public ICollection<Stock> Stocks { get; set; }
        public ICollection<Recipe> Recipes { get; set; }

    }
}