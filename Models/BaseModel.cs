namespace NutriCook_AI_WebAPI.Models
{
    public class BaseModel
    {

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdateAt { get; set; }

    }
}
