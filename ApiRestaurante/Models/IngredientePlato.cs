using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models;

public partial class IngredientePlato
{
    public int Id { get; set; }

    public int? Idplato { get; set; }

    public int? Idingrediente { get; set; }

    public virtual Ingrediente? IdingredienteNavigation { get; set; }

    public virtual Plato? IdplatoNavigation { get; set; }
}
