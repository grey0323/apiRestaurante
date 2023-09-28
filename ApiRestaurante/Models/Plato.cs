using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models;

public partial class Plato
{
    public int Id { get; set; }

    public string? Nombre { get; set; }

    public double? Precio { get; set; }

    public int? NumeroPersonas { get; set; }

    public string? Categoria { get; set; }

    public virtual ICollection<IngredientePlato> IngredientePlatos { get; set; } = new List<IngredientePlato>();

    public virtual ICollection<OrdenPlato> OrdenPlatos { get; set; } = new List<OrdenPlato>();
}
