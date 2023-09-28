namespace ApiRestaurante.Dtos
{
    public class MesaDto
    {
        public int Id { get; set; }

        public int? CantidadPerson { get; set; }

        public string? Descripcion { get; set; }

        public int? IdEstado { get; set; }
    }
}
