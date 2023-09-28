using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models;

public partial class Ordene
{
    public int Id { get; set; }

    public int? MesaPertenece { get; set; }

    public double? Subtotal { get; set; }

    public bool? Estado { get; set; }

    public virtual Mesa? MesaPerteneceNavigation { get; set; }

    public virtual ICollection<OrdenPlato> OrdenPlatos { get; set; } = new List<OrdenPlato>();
}
