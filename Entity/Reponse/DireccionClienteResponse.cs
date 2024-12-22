namespace Entity.Reponse
{
    public class DireccionClienteResponse
    {
        public int IdDireccion { get; set; }
        public string NombreDireccion { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
    }
}
