namespace ApiRestaurante.Dtos
{
    public class OrdenesDtosGet
    {
        public int Id { get; set; }
        public string? MesaPertenece { get; set; }
        public double? Subtotal { get; set; }
        public string? Estado { get; set; }
        public List<string> PlatoSeleccionados { get; set; } = new List<string>();
    }
}
