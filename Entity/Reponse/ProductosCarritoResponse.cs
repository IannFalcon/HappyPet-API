namespace Entity.Reponse
{
    public class ProductosCarritoResponse
    {
        public int IdProducto { get; set; }
        public string? RutaImagen { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
    }
}
