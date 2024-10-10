using System;
using System.Collections.Generic;

namespace FinalProjectBackend.Models;

public partial class RecipeIngredient
{
    public int Id { get; set; }

    public int? RecipeId { get; set; }

    public int ProductId { get; set; }

    public string? Name { get; set; }

    public double? Amount { get; set; }

    public string? Unit { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
