using FinalProjectBackend.Models;

namespace FinalProjectBackend.Services;

public class SpoonacularService
{
    private readonly HttpClient client;
    private readonly IConfiguration config;
   
    public SpoonacularService(HttpClient client, IConfiguration config)
    {
        this.client = client;
        this.config = config;
        client.BaseAddress = new Uri("https://api.spoonacular.com/");
    }

    public Task<SearchResults?> GetResults(string food)
    {
        return client.GetFromJsonAsync<SearchResults>($"food/ingredients/search?apiKey={config["apiKey"]}&query={food}");
    }

    public Task<IngredientInfo?> GetIngredient(string id, float amount = float.NaN, string? unit = null)
    {
        if (!float.IsNaN(amount) && unit != null)
            return client.GetFromJsonAsync<IngredientInfo>($"food/ingredients/{id}/information?amount={amount}&unit={unit}&apiKey={config["apiKey"]}");

        return client.GetFromJsonAsync<IngredientInfo>($"food/ingredients/{id}/information?apiKey={config["apiKey"]}");
    }
}