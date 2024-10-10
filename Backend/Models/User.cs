using System;
using System.Collections.Generic;

namespace FinalProjectBackend.Models;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? Diabetic { get; set; }

    public bool? Gluten { get; set; }

    public bool? Mediterranean { get; set; }

    public bool? Vegan { get; set; }

    public bool? Vegetarian { get; set; }

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
