using System;
using System.Collections.Generic;

namespace ApiRestaurante.Models;

public partial class Mesa
{
    public int Id { get; set; }

    public int? CantidadPerson { get; set; }

    public string? Descripcion { get; set; }

    public int? IdEstado { get; set; }

    public virtual TblEstado? IdEstadoNavigation { get; set; }

    public virtual ICollection<Ordene> Ordenes { get; set; } = new List<Ordene>();
}
