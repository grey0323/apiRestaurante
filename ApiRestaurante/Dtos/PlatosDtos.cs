namespace ApiRestaurante.Dtos
{
    public class PlatosDtos
    {
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public double? Precio { get; set; }

        public int? NumeroPersonas { get; set; }

        public string? Categoria { get; set; }

        public List<string> ingredientes { get; set; } = new List<string>();


    }
}
