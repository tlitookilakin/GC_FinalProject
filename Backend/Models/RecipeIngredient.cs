using System.Text.Json.Serialization;

namespace FinalProjectBackend.Models;

public partial class RecipeIngredient
{
    public int Id { get; set; }

    [JsonIgnore]
    public int? RecipeId { get; set; }

    public int ProductId { get; set; }

    public string? Name { get; set; }

    public double? Amount { get; set; }

    public string? Unit { get; set; }

    [JsonIgnore]

    public virtual Recipe? Recipe { get; set; }
}
