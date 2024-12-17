namespace Entity.Request
{
    public class CrearCuentaRequest
    {
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty;
        public string ApellidoMaterno { get; set; } = string.Empty;
        public int IdTipoDoc { get; set; }
        public string NroDocumento { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public string ConfirmarContrasenia { get; set; } = string.Empty;
    }
}
