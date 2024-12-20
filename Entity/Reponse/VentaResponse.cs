namespace Entity.Reponse
{
    public class VentaResponse
    {
        public int IdVenta { get; set; }
        public string IdTransaccion { get; set; } = string.Empty;
        public string NombreCliente { get; set; } = string.Empty;
        public string DireccionEnvio { get; set; } = string.Empty;
        public int TotalProductos { get; set; }
        public decimal MontoTotal { get; set; }
        public DateTime FecVenta { get; set; }

    }
}
