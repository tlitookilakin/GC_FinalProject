using System.Drawing;

namespace FinalProjectBackend.Models;

public class FoodDetails
{
	public string code { get; set; }
	public Product product { get; set; }
	public int status { get; set; }
	public string status_verbose { get; set; }
}

public class Product
{
	public string _id { get; set; }
	public string[] _keywords { get; set; }
	public string abbreviated_product_name { get; set; }
	public int additives_n { get; set; }
	public string[] additives_original_tags { get; set; }
	public string[] additives_prev_original_tags { get; set; }
	public string[] additives_tags { get; set; }
	public string allergens { get; set; }
	public string allergens_from_ingredients { get; set; }
	public string allergens_from_user { get; set; }
	public string[] allergens_hierarchy { get; set; }
	public string[] allergens_tags { get; set; }
	public string brands { get; set; }
	public string[] brands_tags { get; set; }
	public string code { get; set; }
	public string compared_to_category { get; set; }
	public string customer_service { get; set; }
	public string food_groups { get; set; }
	public string[] food_groups_tags { get; set; }
	public int fruitsvegetablesnuts_100g_estimate { get; set; }
	public string generic_name { get; set; }
	public string id { get; set; }
	public string image_front_small_url { get; set; }
	public string image_front_thumb_url { get; set; }
	public string image_front_url { get; set; }
	public string image_nutrition_small_url { get; set; }
	public string image_nutrition_thumb_url { get; set; }
	public string image_nutrition_url { get; set; }
	public string image_small_url { get; set; }
	public string image_thumb_url { get; set; }
	public string image_url { get; set; }
	public Dictionary<string, ImageDefinition> images { get; set; }
	public Ingredient[] ingredients { get; set; }
	public Dictionary<string ,string> ingredients_analysis { get; set; }
	public string[] ingredients_analysis_tags { get; set; }
	public string[] ingredients_hierarchy { get; set; }
	public string[] ingredients_tags { get; set; }
	public string ingredients_text { get; set; }
	public string ingredients_text_with_allergens { get; set; }
	public string[] labels_hierarchy { get; set; }
	public string[] labels_tags { get; set; }
	public string link { get; set; }
	public object[] minerals_tags { get; set; }
	public string[] misc_tags { get; set; }
	public string no_nutrition_data { get; set; }
	public Dictionary<string, string> nutrient_levels { get; set; }
	public Dictionary<string, float> nutriments { get; set; }
	public string nutrition_data { get; set; }
	public string nutrition_data_per { get; set; }
	public string nutrition_data_prepared { get; set; }
	public string nutrition_data_prepared_per { get; set; }
	public string obsolete { get; set; }
	public object[] other_nutritional_substances_tags { get; set; }
	public string product_name { get; set; }
	public string product_quantity { get; set; }
	public string serving_quantity { get; set; }
	public string serving_size { get; set; }
	public int sortkey { get; set; }
	public string stores { get; set; }
	public string[] stores_tags { get; set; }
	public string traces { get; set; }
	public string traces_from_ingredients { get; set; }
	public string traces_from_user { get; set; }
	public object[] traces_hierarchy { get; set; }
	public object[] traces_tags { get; set; }
	public string update_key { get; set; }
	public object[] vitamins_prev_tags { get; set; }
	public object[] vitamins_tags { get; set; }
}

public class ImageDefinition
{
	public Dictionary<string, Point> sizes { get; set; }
	public string uploaded_t { get; set; }
	public string uploader { get; set; }
}

public class Ingredient
{
	public string id { get; set; }
	public float percent_estimate { get; set; }
	public int percent_max { get; set; }
	public int percent_min { get; set; }
	public string text { get; set; }
	public string vegan { get; set; }
	public string vegetarian { get; set; }
	public string from_palm_oil { get; set; }
	public int percent { get; set; }
	public Ingredient[] ingredients { get; set; }
}
