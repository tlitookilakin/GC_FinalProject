using System.Text.Json.Serialization;

namespace FinalProjectBackend.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? UserId { get; set; }
    public int Servings { get; set; }
    public int? Priority { get; set; }
    public int Calories { get; set; } = 0;
    public double Carbs { get; set; } = 0.0;

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    [JsonIgnore]
    public virtual User? User { get; set; }
}
