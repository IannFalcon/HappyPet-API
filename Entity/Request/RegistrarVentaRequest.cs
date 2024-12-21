namespace Entity.Request
{
    public class RegistrarVentaRequest
    {
        public int IdCliente { get; set; }
        public string IdTransaccion { get; set; } = string.Empty;
        public string PayerID { get; set; } = string.Empty;
        public decimal TotalPago { get; set; }
        public string Pais { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
    }
}
