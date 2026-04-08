using System.ComponentModel.DataAnnotations;

namespace NutriCook_AI_WebAPI.DTOs.Users
{
    public class UserCreateDTO
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
