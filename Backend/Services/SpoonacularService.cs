using FinalProjectBackend.Models;

namespace FinalProjectBackend.Services;

public class SpoonacularService
{
    private readonly HttpClient client;
   
    public SpoonacularService(HttpClient client)
    {
        this.client = client;
        client.BaseAddress = new Uri("https://api.spoonacular.com/");
    }

    public Task<SearchResults?> GetResults(string food)
    {
        return client.GetFromJsonAsync<SearchResults>($"food/ingredients/search?apiKey={Secrets.apiKey}&query={food}");
    }
}