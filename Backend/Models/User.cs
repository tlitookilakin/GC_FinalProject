using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FinalProjectBackend.Models;

public partial class User
{
    public string Id { get; set; } = "";

    public string? Name { get; set; }

    public bool? Diabetic { get; set; }

    public bool? Gluten { get; set; }

    public bool? Mediterranean { get; set; }

    public bool? Vegan { get; set; }

    public bool? Vegetarian { get; set; }

    [JsonIgnore]
    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
