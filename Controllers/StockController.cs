using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NutriCook_AI_WebAPI.DTOs.AIRecipe;
using NutriCook_AI_WebAPI.DTOs.Recipe;
using NutriCook_AI_WebAPI.DTOs.Stock;
using NutriCook_AI_WebAPI.ExternalServices;
using NutriCook_AI_WebAPI.Interfaces.IServices;
using NutriCook_AI_WebAPI.Models;

namespace NutriCook_AI_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {

        private readonly IStockService _stockService;
        private readonly IAIRecipeGenerator _aiRecipeGenerator;

        public StockController(IStockService stockService, IAIRecipeGenerator aiRecipeGenerator)
        {
            this._stockService = stockService;
            this._aiRecipeGenerator = aiRecipeGenerator;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateStock([FromBody] StockCreateDTO stock)
        {

            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (userIdClaim == null)
                    return Unauthorized("User not found in token");

                var userId = int.Parse(userIdClaim);

                var newStock = new Stock
                {
                    Name = stock.Name,
                    Quantity = stock.Quantity,
                    Description = stock.Description,
                    UserId = userId
                };

                await _stockService.CreateAsync(newStock);

                return Ok(new { message = "Stock added successfully" });
            }
            catch (Exception err)
            {

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


        [HttpGet("user")]
        public async Task<IActionResult> GetAllStockByUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized("User not found in token");

            var userId = int.Parse(userIdClaim);

            var stocks = await this._stockService.GetStocksByUserId(userId);

            return Ok(stocks);
        }


    }
}
