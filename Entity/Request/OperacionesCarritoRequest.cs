namespace Entity.Request
{
    public class OperacionesCarritoRequest
    {
        public int IdCliente { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public bool Accion { get; set; }
    }
}
