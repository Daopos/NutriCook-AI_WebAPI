using System.ComponentModel.DataAnnotations;

namespace NutriCook_AI_WebAPI.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public int Quantity { get; set; } = 0;


    }
}
