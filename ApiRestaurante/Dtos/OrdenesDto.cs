namespace ApiRestaurante.Dtos
{
    public class OrdenesDto
    {
        public int Id { get; set; }

        public int? MesaPertenece { get; set; }

        public double? Subtotal { get; set; }

        public bool? Estado { get; set; }

        public List<string> PlatoSeleccionados { get; set; } = new List<string>();

        public string Mesa { get; set; }
    }
}
