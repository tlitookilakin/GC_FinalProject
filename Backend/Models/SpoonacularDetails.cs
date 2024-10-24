namespace FinalProjectBackend.Models;

public class SearchResults
{
	public Results[] results { get; set; }
	public int offset { get; set; }
	public int number { get; set; }
	public int totalResults { get; set; }
}

public class Results
{
	public int id { get; set; }
	public string name { get; set; }
	public string image { get; set; }
}

public class RecipeResults
{
	public int id { get; set; }
	public string title { get; set; }
	public string image { get; set; }
}

public class RecipeSearchResults
{
	public RecipeResults[] results { get; set; }
	public int offset { get; set; }
	public int number { get; set; }
	public int totalResults { get; set; }
}

public class IngredientInfo
{
	public int id { get; set; }
	public string original { get; set; }
	public string originalName { get; set; }
	public string name { get; set; }
	public float amount { get; set; }
	public string unit { get; set; }
	public string unitShort { get; set; }
	public string unitLong { get; set; }
	public string[] possibleUnits { get; set; }
	public string consistency { get; set; }
	public string[] shoppingListUnits { get; set; }
	public string aisle { get; set; }
	public string image { get; set; }
	public object[] meta { get; set; }
	public Nutrition nutrition { get; set; }
	public string[] categoryPath { get; set; }
}

public class Nutrition
{
	public Nutrient[] nutrients { get; set; }
	public Nutrient[] properties { get; set; }
	public Nutrient[] flavonoids { get; set; }
	public Caloricbreakdown caloricBreakdown { get; set; }
	public Nutrient weightPerServing { get; set; }
}

public class Caloricbreakdown
{
	public float percentProtein { get; set; }
	public float percentFat { get; set; }
	public float percentCarbs { get; set; }
}

public class Nutrient
{
	public string name { get; set; }
	public float amount { get; set; }
	public string unit { get; set; }
	public float percentOfDailyNeeds { get; set; }
}



