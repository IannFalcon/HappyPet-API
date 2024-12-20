namespace Entity.Reponse
{
    public class DatosProveedorResponse
    {
        public int IdProveedor { get; set; }
        public string RucProveedor { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        public string NroTelefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public DateTime FecRegistro { get; set; }
    }
}
