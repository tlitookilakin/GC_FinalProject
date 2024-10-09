using FinalProjectBackend.Models;

namespace FinalProjectBackend.Services
{
	public class OpenFoodService
	{
		private readonly HttpClient client;
		public OpenFoodService(HttpClient client)
		{
			this.client = client;
			client.BaseAddress = new Uri("https://world.openfoodfacts.net/api/v2/");
			client.DefaultRequestHeaders.UserAgent.TryParseAdd("grandcircus.MyFoodPage/1.0");
		}

		public Task<FoodDetails?> GetByUPC(string upc)
		{
			return client.GetFromJsonAsync<FoodDetails>($"product/{upc}");
		}
	}
}
