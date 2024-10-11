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

    public Task<IngredientInfo?> GetIngredient(string id, float amount = 0f, string? unit = null)
    {
        if (amount != 0 && unit != null)
            return client.GetFromJsonAsync<IngredientInfo>($"food/ingredients/{id}/information?amount={amount}&unit={unit}&apiKey={config["apiKey"]}");

        return client.GetFromJsonAsync<IngredientInfo>($"food/ingredients/{id}/information?apiKey={config["apiKey"]}");
    }
    public Task<IngredientInfo[]> ParseIngredient(IList <string> strings, float serving)
    {
        string ingredientList = string.Join('\n', strings);
        ingredientList = Uri.EscapeDataString(ingredientList);
        return client.GetFromJsonAsync<IngredientInfo[]>($"food/recipes/parseIngredients?servings=1&includeNutrition=true&language=en&ingredientList={ingredientList}");

    }
}

