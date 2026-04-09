using Newtonsoft.Json;
using System.Text;
using NutriCook_AI_WebAPI.DTOs.AIRecipe;
using NutriCook_AI_WebAPI.Interfaces.IRepo;
using System.Text.Json;

namespace NutriCook_AI_WebAPI.ExternalServices
{
    public class AIRecipeGenerator : IAIRecipeGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IRecipeRepo _recipeRepo;
        private readonly IStockRepo _stockRepo;


        public AIRecipeGenerator(HttpClient httpClient, IConfiguration config, IRecipeRepo recipeRepo, IStockRepo stockRepo)
        {
            _httpClient = httpClient;
            _apiKey = config["Gemini:ApiKey"];
            _recipeRepo = recipeRepo;
            _stockRepo = stockRepo;
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

            return new AIRecipeResponse
            {
                Title = title,
                Difficulty = difficulty,
                Description = description
            };
        }


        public async Task<IEnumerable<AIRecipeResponse>> GenerateRecipeBaseByStock(int userId)
        {

            var stocks = await _stockRepo.GetAllStocksWithQuantityAsync(userId);

            var ingredientsText = string.Join("\n", stocks.Select(s => $"{s.Name} - {s.Quantity}"));

            var url = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-3.1-flash-lite-preview:generateContent?key={_apiKey}";

            var requestBody = new
            {
                contents = new[]
                {
            new
            {
                parts = new[]
                {
                    new
                    {
                        text = $@"
                                Generate 7 recipes based on the following ingredients and quantities:

                                {ingredientsText}

                                Rules:
                                - Use the ingredients based on available quantity
                                - It is okay if not all ingredients are used
                                - Prefer recipes that match the quantities


                                Return ONLY valid JSON:

                                [
                                  {{
                                    ""title"": ""value"",
                                    ""difficulty"": ""value"",
                                    ""description"": ""value""
                                  }}
                                ]

                                Rules:
                                - Return exactly 7 items
                                - No extra text
                                - No markdown
                                - No emojis
                                "
                    }
                }
            }
                }
            };


            // Use DI HttpClient (IMPORTANT)
            var response = await _httpClient.PostAsJsonAsync(url, requestBody);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Gemini API failed: {response.StatusCode}, {responseContent}");
            }

            // Parse Gemini response
            using var doc = JsonDocument.Parse(responseContent);

            var text = doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (string.IsNullOrWhiteSpace(text))
                return new List<AIRecipeResponse>();

            // Clean possible markdown wrappers
            text = text.Replace("```json", "").Replace("```", "").Trim();

            // Deserialize into your model
            var recipes = System.Text.Json.JsonSerializer.Deserialize<List<AIRecipeResponse>>(
                text,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return recipes;
        }


    }
}
