using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models;

public partial class OrdenPlato
{
    public int Id { get; set; }

    public int? Idplato { get; set; }

    public int? Idorden { get; set; }

    public virtual Ordene? IdordenNavigation { get; set; }

    public virtual Plato? IdplatoNavigation { get; set; }
}
