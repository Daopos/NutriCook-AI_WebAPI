using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Text;
using NutriCook_AI_WebAPI.DTOs.AIRecipe;

namespace NutriCook_AI_WebAPI.ExternalServices
{
    public class AIRecipeGenerator : IAIRecipeGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public AIRecipeGenerator(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"];
        }
        public async Task<AIRecipeResponse> GenerateRecipe(AIRecipeRequest req)
        {
            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";
            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = req.Ingredients }
                    }
                }
            }
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var response = await _httpClient.PostAsync(url,
                new StringContent(json, Encoding.UTF8, "application/json"));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API failed: {response.StatusCode}");
            }

            var result = await response.Content.ReadAsStringAsync();

            dynamic data = JsonConvert.DeserializeObject(result);

            string text = data?.candidates?[0]?.content?.parts?[0]?.text ?? "No response";

            return new AIRecipeResponse
            {
                Description = text
            };
        }



    }
}
