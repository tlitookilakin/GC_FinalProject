using System;
using System.Collections.Generic;

namespace FinalProjectBackend.Models;

public partial class Recipe
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<RecipeIngredient> RecipeIngredients { get; set; } = new List<RecipeIngredient>();

    public virtual User? User { get; set; }
}
