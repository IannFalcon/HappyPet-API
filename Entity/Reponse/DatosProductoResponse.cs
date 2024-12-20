namespace Entity.Reponse
{
    public class DatosProductoResponse
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public CategoriaResponse Categoria { get; set; } = new CategoriaResponse();
        public MarcaResponse Marca { get; set; } = new MarcaResponse();
        public string Descripcion { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public int Stock { get; set; }
        public string? NombreImagen { get; set; }
        public string? RutaImagen { get; set; }
        public DateTime? FecVencimiento { get; set; }
        public DateTime FecRegistro { get; set; }
    }
}
