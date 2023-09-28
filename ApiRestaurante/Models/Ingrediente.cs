using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models;

public partial class Ingrediente
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public virtual ICollection<IngredientePlato> IngredientePlatos { get; set; } = new List<IngredientePlato>();
}
