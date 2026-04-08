using System.ComponentModel.DataAnnotations;

namespace NutriCook_AI_WebAPI.DTOs.Users
{
    public class UserReadDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

    }
}
