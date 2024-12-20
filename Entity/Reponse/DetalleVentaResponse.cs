namespace Entity.Reponse
{
    public class DetalleVentaResponse
    {
        public int Cantidad { get; set; }
        public string NombreProducto { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
