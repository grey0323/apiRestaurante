using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models;

public partial class TblEstado
{
    public int Id { get; set; }

    public string Estado { get; set; } = null!;

    public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();
}
