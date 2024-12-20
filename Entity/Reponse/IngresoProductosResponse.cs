namespace Entity.Reponse
{
    public class IngresoProductosResponse
    {
        public string NombreProveedor { get; set; } = string.Empty;
        public string NombreProducto { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public DateTime FecIngreso { get; set; }
    }
}
