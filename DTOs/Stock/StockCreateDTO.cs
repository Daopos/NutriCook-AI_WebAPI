namespace NutriCook_AI_WebAPI.DTOs.Stock
{
    public class StockCreateDTO
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public string? Description { get; set; }
    }
}
