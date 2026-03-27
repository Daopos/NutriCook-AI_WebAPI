using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutriCook_AI_WebAPI.Interfaces.IServices;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            this._stockService = stockService;  
        }

        [HttpPost]
        public async Task<IActionResult> CreateStock([FromBody] Stock stock)
        {
           
            try
            {
                await this._stockService.CreateAsync(stock);

                return Ok("Stock added successfully");
            }
            catch (Exception err) 
            {
                Console.WriteLine("Error adding stock", err);

                return StatusCode(500, "Something went wrong on the server");

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStock()
        {

            var stocks = await this._stockService.GetAllAsync();

            return Ok(stocks);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockById(int id)
        {

            await this._stockService.DeleteAsync(id);

            return NoContent();
        }


    }
}
