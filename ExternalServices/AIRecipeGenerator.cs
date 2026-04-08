using Newtonsoft.Json;
using System.Text;
using NutriCook_AI_WebAPI.DTOs.AIRecipe;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using NutriCook_AI_WebAPI.Models;
using NutriCook_AI_WebAPI.DTOs.Recipe;

namespace NutriCook_AI_WebAPI.ExternalServices
{
    public class AIRecipeGenerator : IAIRecipeGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public readonly IRecipeRepo _recipeRepo;

        public AIRecipeGenerator(HttpClient httpClient, IConfiguration config, IRecipeRepo recipeRepo)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"];
            _recipeRepo = recipeRepo;
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
                        new { text = $@"
                            Generate a recipe based on these ingredients: {req.Ingredients}

                            Return ONLY valid in this format:

                              title: <value>,
                              difficulty: <value>,
                              description: <value>

                            Return ONLY on this format. No explanations. No markdown. No code blocks. no bullet just a paragaph, no emojies
                            or something that a database can't save
                            " }
                    }
                }
            }
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var response = await _httpClient.PostAsync(
                url,
                new StringContent(json, Encoding.UTF8, "application/json")
            );

            // ❗ Always check first
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"Gemini API failed: {response.StatusCode}, {errorContent}");
            }

            var result = await response.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(result);

            // Extract text safely
            string text = data?.candidates?[0]?.content?.parts?[0]?.text;

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new Exception("No response from Gemini.");
            }

            // Normalize lines
            var lines = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);

            // Helper function to extract values
            string Extract(string key)
            {
                return lines
                    .FirstOrDefault(l => l.StartsWith(key, StringComparison.OrdinalIgnoreCase))
                    ?.Substring(key.Length)   // removes "title:"
                    .Trim();
            }

            // Get values
            string title = Extract("title:");
            string difficulty = Extract("difficulty:");
            string description = Extract("description:");

            //var newRecipe = await _recipeRepo.CreateNewRecipe(new Recipe
            //{
            //    Title = title,
            //    Difficulty = difficulty,
            //    Description = description
            //});


            return new AIRecipeResponse
            {
                Title = title,
                Difficulty = difficulty,    
                Description = description
            };
        }



    }
}
