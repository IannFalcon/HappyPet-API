namespace Entity.Request
{
    public class CambiarContraseniaNuevoUsuarioRequest
    {
        public int IdUsuario { get; set; }
        public string NuevaContrasenia { get; set; } = string.Empty;
        public string ConfirmarContrasenia { get; set; } = string.Empty;
    }
}
